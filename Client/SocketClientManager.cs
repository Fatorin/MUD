using Client.Base;
using Client.ClientSystem;
using Common;
using Common.Model.Command;
using Common.Model.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class SocketClientManager
    {
        public static SocketClientManager Instance { get; } = new SocketClientManager();
        private static ConcurrentDictionary<int, BaseClientSystem> SystemDict;
        private static int serverPort;
        private static Socket socketClient;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public SocketClientManager()
        {
            var rand = new Random().Next(1, 3);
            serverPort = GlobalSetting.PortNum1;
            SystemDict = new ConcurrentDictionary<int, BaseClientSystem>();
            SystemDict.TryAdd((int)SystemCategory.LoginSystem, LoginClientSystem.Instance);
            SystemDict.TryAdd((int)SystemCategory.MessageSystem, MessageClientSystem.Instance);
            SystemDict.TryAdd((int)SystemCategory.PlayerSystem, PlayerClientSystem.Instance);
            /*if (rand == 1)
            {
                serverPort = GlobalSetting.PortNum1;
            }
            else
            {
                serverPort = GlobalSetting.PortNum2;
            }*/
        }

        public void StartClientAndLogin(User userInfo)
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverPort);

                // Create a TCP/IP socket.  
                socketClient = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                socketClient.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), socketClient);
                connectDone.WaitOne();

                // Send test data to the remote device.
                Send(socketClient, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginReq, UserLoginReqPayload.CreatePayload(userInfo)));
                sendDone.WaitOne();

                // Receive the response from the remote device.
                Receive(socketClient);
                receiveDone.WaitOne();
            }
            catch (Exception e)
            {
                Program.mainUI.ShowLogOnResult(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Program.mainUI.ShowLogOnResult($"Socket connected to {client.RemoteEndPoint.ToString()}");

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Program.mainUI.ShowLogOnResult(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                Packet packetObj = new Packet();
                packetObj.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(packetObj.buffer, 0, Packet.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), packetObj);
            }
            catch (Exception e)
            {
                Program.mainUI.ShowLogOnResult(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                Packet packetObj = (Packet)ar.AsyncState;
                Socket client = packetObj.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    if (!packetObj.isCorrectPack)
                    {
                        PacketBuilder.UnPackParam(packetObj.buffer, out var crc, out var dataLen, out var category, out var command);
                        if (crc == PacketBuilder.crcCode)
                        {
                            packetObj.SetFirstReceive(dataLen, category, command);
                        }
                        else
                        {
                            //如果CRC不對就不動作(先不關閉)
                            Program.mainUI.ShowLogOnResult("CRC check fail");
                            throw new Exception();
                        }
                    }
                    //將收到的封包複製到infoBytes，從最後收到的位置
                    Array.Copy(packetObj.buffer, 0, packetObj.infoBytes, packetObj.LastReceivedPos, bytesRead);
                    //減去已收到的封包數
                    //接收完後更新對應的LastReceivedPos
                    packetObj.SetContiuneReceive(bytesRead);

                    if (packetObj.ReceiveLen == 0)
                    {
                        //傳送資料給對應的Command，扣掉前面的CRC,DataLen,Command
                        var pack = packetObj.infoBytes.Skip(PacketBuilder.VerificationLen).ToArray();
                        Program.mainUI.ShowLogOnResult($"SystemCategory={packetObj.SystemCategory}, SystemCommand={packetObj.SystemCommand}");
                        if (SystemDict.TryGetValue(packetObj.SystemCategory, out var baseClientSystem))
                        {
                            baseClientSystem.PlayerEnter(packetObj.SystemCommand, pack);
                        }
                        else
                        {
                            Program.mainUI.ShowLogOnResult("Not mapping function.");
                        }

                        //接收完成
                        receiveDone.Set();
                        //開始接收新的封包
                        Receive(socketClient);
                        receiveDone.WaitOne();
                    }
                    else
                    {
                        client.BeginReceive(packetObj.buffer, 0, Packet.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), packetObj);
                    }
                }
            }
            catch (Exception e)
            {
                Program.mainUI.ShowLogOnResult(e.ToString());
            }
        }

        public void Send(Socket client, byte[] byteData)
        {
            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Program.mainUI.ShowLogOnResult("發送失敗");
                Program.mainUI.ShowLogOnResult(e.ToString());
            }
        }

        private void SocketShutDown(Socket socket)
        {
            // Release the socket.  
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
