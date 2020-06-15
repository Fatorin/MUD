using Common;
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
using System.Reflection;
using System.Text;
using System.Threading;

namespace Server
{
    public sealed class SockerManager
    {
        public static SockerManager Instance { get; } = new SockerManager();
        private static ConcurrentDictionary<string, Player> PlayerConnectDict;
        private static ConcurrentDictionary<int, BaseSystem> SystemDict;
        private static int UsePort;
        private static ApplicationContext dbContext;
        // Thread signal.  
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public SockerManager()
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
        }

        public ApplicationContext GetApplicationContext()
        {
            return dbContext;
        }

        public void CreatePlayer(Player player)
        {
            PlayerConnectDict.TryAdd(player.PlayerId, player);
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

        private void AcceptCallback(IAsyncResult ar)
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

        private void ReadCallback(IAsyncResult ar)
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
                        PacketBuilder.UnPackParam(packetObj.buffer, out var crc, out var dataLen, out var systemCategory, out var systemCommand);
                        if (crc == PacketBuilder.crcCode)
                        {
                            //依照第一筆封包做初始化的行為
                            packetObj.SetFirstReceive(dataLen, systemCategory, systemCommand);
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
                        //想一個東西能把資料丟給對應的SYSTEM
                        if (SystemDict.TryGetValue(packetObj.SystemCategory, out var system))
                        {
                            //傳送資料給對應的Command，扣掉前面的CRC,DataLen,Command
                            var pack = packetObj.infoBytes.Skip(PacketBuilder.VerificationLen).ToArray();
                            system.PlayerEnter(null, packetObj.SystemCommand, pack);
                        }
                        else
                        {
                            //有對應的Function就執行，沒對應的Func就報錯但會繼續執行
                            Console.WriteLine("Not mapping system.");
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

        public void Send(Player player, byte[] byteData)
        {
            var handler = player.Connection;
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        public void Broadcast(byte[] byteData)
        {
            foreach (var ClientConnectInfo in PlayerConnectDict)
            {
                Send(ClientConnectInfo.Value, byteData);
            }
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


        private void ExceptionDisconnect(Socket handler)
        {
            Console.WriteLine($"Remove {handler.RemoteEndPoint}, AcceptCount:{PlayerConnectDict.Count}");
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        private void ManualDisconnect(String username)
        {
            try
            {
                if (!PlayerConnectDict.TryRemove(username, out var player))
                {
                    Console.WriteLine($"[{username}] not find.");
                    return;
                }
                Console.WriteLine($"[{username}] repeat login , remove connect.");
                ExceptionDisconnect(player.Connection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void SubscribeToRedis()
        {
            //註冊Redis的事件
            var sub = RedisHelper.Connection.GetSubscriber();
            sub.Subscribe("messages", (channel, message) =>
            {
                //當收到Message的時候，會使用Send傳送給所有用戶
                var MsgInfoDatas = new Message[]
                {
                    new Message{ MessageString = message }
                };
                MessageSystem.Instance.SendMsgToAll(MsgInfoDatas);
            });

            sub.Subscribe("login", (channel, loginUsername) =>
            {
                //當收到登入訊息後，會先通知踢掉，再進行後續登入吧(?
                ManualDisconnect(loginUsername);
            });
        }

        public void PublishMessageToRedis(String message)
        {
            //註冊Redis的事件
            var sub = RedisHelper.Connection.GetSubscriber();
            sub.Publish("messages", message);
        }

        public void PublishLoginToRedis(String loginUsername)
        {
            //註冊Redis的事件
            var sub = RedisHelper.Connection.GetSubscriber();
            sub.Publish("login", loginUsername);
        }

        private bool PortInUse(int port)
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
            PlayerConnectDict = new ConcurrentDictionary<string, Player>();
            SystemDict = new ConcurrentDictionary<int, BaseSystem>();
            //這邊綁定至對應的SYSTEM
            SystemDict.TryAdd((int)SystemCategory.LoginSystem, LoginSystem.Instance);
            SystemDict.TryAdd((int)SystemCategory.MessageSystem, MessageSystem.Instance);
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
            MessageSystem.Instance.SaveMultiInfoDataToRedis(RedisHelper.GetRedisDb(RedisHelper.RedisDbNum.MsgData), dataList);
        }

    }
}
