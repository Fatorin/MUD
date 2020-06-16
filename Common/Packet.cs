using System;
using System.Net.Sockets;

namespace Common
{
    public class Packet
    {
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 32;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        public byte[] infoBytes;
        public int ReceiveLen;
        public int LastReceivedPos;
        public bool isCorrectPack = false;
        public int SystemCategory;
        public int SystemCommand;
        public int playerUid;

        public void ResetData()
        {
            ReceiveLen = 0;
            infoBytes = null;
            isCorrectPack = false;
            LastReceivedPos = 0;
            SystemCategory = 0;
            SystemCommand = 0;
            playerUid = 0;
        }

        public void SetFirstReceive(int dataLen, int uid, int systemCategory, int systemCommand)
        {
            //設定封包驗證通過(如果有要接收第二段就會繼續接收)
            isCorrectPack = true;
            //設定封包的長度(第一次的時候)，要減少前面的crc dataLen command
            ReceiveLen = dataLen;
            //設定接收封包大小
            infoBytes = new byte[dataLen];
            //設定封包的指令(第一次的時候)
            playerUid = uid;
            SystemCategory = systemCategory;
            SystemCommand = systemCommand;
        }

        public void SetContiuneReceive(int bytesRead)
        {
            //減去已收到的封包數
            //接收完後更新對應的LastReceivedPos
            ReceiveLen -= bytesRead;
            LastReceivedPos += bytesRead;
        }
    }
}
