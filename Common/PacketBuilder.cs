using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common
{
    public class PacketBuilder
    {
        public static readonly int crcCode = 65517;
        public static readonly int VerificationLen = 16;
        public static byte[] BuildPacket(int playerUid, int systemCategory, int systemCommand, byte[] dataByte)
        {
            //定義
            byte[] crcByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(crcCode));
            byte[] playerUidByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(playerUid));
            byte[] systemCategoryByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(systemCategory));
            byte[] systemCommandByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(systemCommand));
            byte[] packByte = new byte[crcByte.Length + playerUidByte.Length + systemCategoryByte.Length + systemCommandByte.Length + dataByte.Length];
            //存入CRC
            crcByte.CopyTo(packByte, 0);
            //存入封包總長
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(packByte.Length)).CopyTo(packByte, 4);
            //存入玩家Id
            playerUidByte.CopyTo(packByte, 8);
            //存入封包的對應系統
            systemCategoryByte.CopyTo(packByte, 12);
            //存入封包的對應系統的指令
            systemCommandByte.CopyTo(packByte, 16);
            //存入封包的資料
            dataByte.CopyTo(packByte, 20);
            //回傳資料
            return packByte;
        }

        public static void UnPackParam(byte[] dataByte, out int crc, out int dataLen, out int playerUid, out int systemCategory, out int systemCommand)
        {
            crc = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 0));
            dataLen = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 4));
            playerUid = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 8));
            systemCategory = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 12));
            systemCommand = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 16));
        }
    }
}
