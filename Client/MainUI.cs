using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainUI : Form
    {
        static Dictionary<int, Action> CommandReqDict = new Dictionary<int, Action>();
        static Dictionary<int, Action<byte[]>> CommandRespDict = new Dictionary<int, Action<byte[]>>();
        static int serverPort;
        static Socket socketClient;

        public MainUI()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void ShowLogOnResult(string str)
        {
            //不同執行序的寫法
            tbResult.InvokeIfRequired(() =>
            {
                tbResult.AppendText(str + Environment.NewLine);
            });
        }

        #region AsynchronousClient

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private void StartClientAndLogin()
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
                //Send(socketClient, PacketBuilder.BuildPacket((int)CommandEnum.LoginAuth, UserReqLoginPayload.CreatePayload(GenUserInfo())));
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(socketClient);
                receiveDone.WaitOne();
            }
            catch (Exception e)
            {
                ShowLogOnResult(e.ToString());
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

                ShowLogOnResult($"Socket connected to {client.RemoteEndPoint.ToString()}");

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                ShowLogOnResult(e.ToString());
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
                ShowLogOnResult(e.ToString());
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
                            ShowLogOnResult("CRC check fail");
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
                        //執行對應的FUNC
                        {
                            ShowLogOnResult("Not mapping function.");
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
                ShowLogOnResult(e.ToString());
            }
        }

        private void Send(Socket client, byte[] byteData)
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
                ShowLogOnResult("發送失敗");
                ShowLogOnResult(e.ToString());
            }
        }

        private void SocketShutDown(Socket socket)
        {
            // Release the socket.  
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        #endregion
    }

    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
