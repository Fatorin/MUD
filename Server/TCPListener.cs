﻿using Common;
using Common.Model.Command;
using Common.Model.Message;
using Common.Model.User;
using Newtonsoft.Json;
using Server.PostgreSQL;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class TCPListener
    {
        private static LoginSystem loginSystem;
        private static MessageSystem messageSystem;
        private static ConcurrentDictionary<string, Socket> ClientConnectDict;
        private static Dictionary<int, Action<Socket, byte[]>> CommandRespDict;
        private static int UsePort;
        private static ApplicationContext dbContext;

        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public TCPListener()
        {
            UsePort = GlobalSetting.PortNum1;
            dbContext = new ApplicationContext();
            InitMapping();
            InitFakeData();
            SubscribeToRedis();
            if (PortInUse(GlobalSetting.PortNum1))
            {
                UsePort = GlobalSetting.PortNum2;
            }

            if (PortInUse(GlobalSetting.PortNum1) && PortInUse(GlobalSetting.PortNum2))
            {
                Console.WriteLine("Port are used.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Port use {UsePort}");
            loginSystem = new LoginSystem();
            messageSystem = new MessageSystem();
        }

        public void StartListening()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, UsePort);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadKey();

        }

        public void AcceptCallback(IAsyncResult ar)
        {

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            Console.WriteLine("Accept one connect.");
            Packet state = new Packet();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, Packet.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            // Signal the main thread to continue.  
            allDone.Set();
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            Packet packetObj = (Packet)ar.AsyncState;
            Socket handler = packetObj.workSocket;
            try
            {

                //從socket接收資料
                int bytesRead = handler.EndReceive(ar);
                //如果有收到的封包才會做事情
                if (bytesRead > 0)
                {
                    if (!packetObj.isCorrectPack)
                    {
                        //檢查是不是正常封包 第一會檢查CRC 有的話就改成TRUE
                        //如果沒有CRC那就直接拒絕接收
                        PacketBuilder.UnPackParam(packetObj.buffer, out var crc, out var dataLen, out var command);
                        if (crc == PacketBuilder.crcCode)
                        {
                            //依照第一筆封包做初始化的行為
                            packetObj.SetFirstReceive(dataLen, command);
                        }
                        else
                        {
                            //如果CRC不對就不動作 應該要踢掉 代表這個使用者有問題
                            Console.WriteLine("CRC check fail, make new exception.");
                            throw new Exception();
                        }
                    }

                    //將收到的封包複製到infoBytes，從最後收到的位置     
                    Array.Copy(packetObj.buffer, 0, packetObj.infoBytes, packetObj.LastReceivedPos, bytesRead);
                    packetObj.SetContiuneReceive(bytesRead);

                    //如果封包都收完了 則執行動作
                    if (packetObj.ReceiveLen == 0)
                    {
                        //執行對應的FUNC
                        if (CommandRespDict.TryGetValue(packetObj.Command, out var mappingFunc))
                        {
                            //傳送資料給對應的Command，扣掉前面的CRC,DataLen,Command
                            mappingFunc(handler, packetObj.infoBytes.Skip(PacketBuilder.VerificationLen).ToArray());
                        }
                        else
                        {
                            //有對應的Function就執行，沒對應的Func就報錯但會繼續執行
                            Console.WriteLine("Not mapping function.");
                        }
                        //清除封包資訊 重設
                        packetObj.ResetData();
                        //重設後再次接收
                        handler.BeginReceive(packetObj.buffer, 0, Packet.BufferSize, 0,
                        new AsyncCallback(ReadCallback), packetObj);
                    }
                    else
                    {
                        // Not all data received. Get more.
                        handler.BeginReceive(packetObj.buffer, 0, Packet.BufferSize, 0,
                        new AsyncCallback(ReadCallback), packetObj);
                    }
                }
            }
            catch (Exception e)
            {
                //接收時如果發生錯誤則做以下處理
                Console.WriteLine(e.ToString());
                if (handler.Connected)
                {
                    ExceptionDisconnect(handler);
                }
            }
        }

        private void Send(Socket handler, byte[] byteData)
        {
            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                ExceptionDisconnect((Socket)ar.AsyncState);
            }
        }

        private void ReceviveLoginAuthData(Socket handler, byte[] byteArray)
        {
            UserReqLoginPayload.ParsePayload(byteArray, out var infoData);
            Console.WriteLine($"Clinet:{handler.RemoteEndPoint} Time：{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}, UserId:{infoData.UserId}");
            var user = dbContext.Users.Find(infoData.UserId);
            //如果用戶不存在則自動幫他創帳號
            if (user == null)
            {
                Console.WriteLine("not found equal userid, created new user.");
                user = new User
                {
                    UserId = infoData.UserId,
                    UserPwd = infoData.UserPwd
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            //建立完帳戶、確認用戶帳密是否一致
            //不一致就傳送失敗訊號，並且剔除使用者
            if (infoData.UserPwd != user.UserPwd)
            {
                Send(handler, PacketBuilder.BuildPacket((int)CommandCategory.LoginAuth, UserRespLoginPayload.CreatePayload(UserAck.AuthFail)));
                return;
            }
            //驗證成功就通知另一個伺服器把人踢了(這邊要用Redis做)
            PublishLoginToRedis(infoData.UserId);
            //要重寫與另一個SERVER溝通的方法
            //回傳成功訊息給對應的人
            Send(handler, PacketBuilder.BuildPacket((int)CommandCategory.LoginAuth, UserRespLoginPayload.CreatePayload(UserAck.Success)));

            //回傳留言版最後一百筆資料            
            Send(handler, PacketBuilder.BuildPacket((int)CommandCategory.MsgAll, MessageRespPayload.CreatePayload(MessageAck.Success, messageSystem.GetLastMessage().ToArray())));
            ClientConnectDict.TryAdd(infoData.UserId, handler);
        }

        private void ReceviveOneMessage(Socket handler, byte[] byteArray)
        {
            MessageReqPayload.ParsePayload(byteArray, out var infoDatas);
            //理論上只有第一筆訊息 懶得分開寫
            //驗證訊息用而已 連這段轉換都不用寫
            Console.WriteLine($"Clinet:{handler.RemoteEndPoint} Time：{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}, Msg:{infoDatas[0].MessageString}");
            //存入Redis
            messageSystem.SaveOneInfoDataToRedis(messageSystem.GetRedisDb(Helper.RedisDbNum.MsgData), infoDatas[0]);
            //丟到Redis發布訊息(因為兩台同時註冊了，避免重送)
            PublishMessageToRedis(infoDatas[0].MessageString);
        }

        private void SendMsgToAll(Message[] infoDatas)
        {
            foreach (var socketTemp in ClientConnectDict)
            {
                //將收到的訊息傳送給所有當前用戶
                Send(socketTemp.Value, PacketBuilder.BuildPacket((int)CommandCategory.MsgOnce, MessageRespPayload.CreatePayload(MessageAck.Success, infoDatas.ToArray())));
            }
        }

        private void ExceptionDisconnect(Socket handler)
        {
            Console.WriteLine($"Remove {handler.RemoteEndPoint}, AcceptCount:{ClientConnectDict.Count}");
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        private void ManualDisconnect(String username)
        {
            try
            {
                if (!ClientConnectDict.TryRemove(username, out var handler))
                {
                    Console.WriteLine($"[{username}] not find.");
                    return;
                }
                Console.WriteLine($"[{username}] repeat login , remove connect.");
                ExceptionDisconnect(handler);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        private void SubscribeToRedis()
        {
            //註冊Redis的事件
            var sub = Helper.Connection.GetSubscriber();
            sub.Subscribe("messages", (channel, message) =>
            {
                //當收到Message的時候，會使用Send傳送給所有用戶
                var MsgInfoDatas = new Message[]
                {
                    new Message{ MessageString = message }
                };
                SendMsgToAll(MsgInfoDatas);
            });

            sub.Subscribe("login", (channel, loginUsername) =>
            {
                //當收到登入訊息後，會先通知踢掉，再進行後續登入吧(?
                //先不要弄
                ManualDisconnect(loginUsername);
            });
        }

        private void PublishMessageToRedis(String message)
        {
            //註冊Redis的事件
            var sub = Helper.Connection.GetSubscriber();
            sub.Publish("messages", message);
        }

        private void PublishLoginToRedis(String loginUsername)
        {
            //註冊Redis的事件
            var sub = Helper.Connection.GetSubscriber();
            sub.Publish("login", loginUsername);
        }

        public bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }

        private void InitMapping()
        {
            ClientConnectDict = new ConcurrentDictionary<string, Socket>();
            CommandRespDict = new Dictionary<int, Action<Socket, byte[]>>()
                {
                    { (int)CommandCategory.LoginAuth, ReceviveLoginAuthData},
                    { (int)CommandCategory.MsgOnce, ReceviveOneMessage},
                };
        }

        private void InitFakeData()
        {
            List<Message> dataList = new List<Message>();
            for (int i = 0; i < 10; i++)
            {
                dataList.Add(new Message
                {
                    MessageString = $"testString{i}"
                });
            }
            messageSystem.SaveMultiInfoDataToRedis(messageSystem.GetRedisDb(Helper.RedisDbNum.MsgData), dataList);
        }

    }
}
