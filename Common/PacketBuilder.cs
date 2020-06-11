﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common
{
    public class PacketBuilder
    {
        public static readonly int crcCode = 65517;
        public static readonly int VerificationLen = 12;
        public static byte[] BuildPacket(int command, byte[] dataByte)
        {
            //定義
            //CRC, command , data
            //依序存入crc len cmd data
            byte[] crcByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(crcCode));
            byte[] commandByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(command));
            byte[] dataLen = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(crcByte.Length + commandByte.Length + dataByte.Length));
            byte[] packByte = new byte[crcByte.Length + dataLen.Length + commandByte.Length + dataByte.Length];
            crcByte.CopyTo(packByte, 0);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(packByte.Length)).CopyTo(packByte, 4);
            commandByte.CopyTo(packByte, 8);
            dataByte.CopyTo(packByte, 12);
            return packByte;
        }

        public static void UnPackParam(byte[] dataByte, out int crc, out int dataLen, out int command)
        {
            crc = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 0));
            dataLen = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 4));
            command = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(dataByte, 8));
        }
    }
}