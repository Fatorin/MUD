using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.GameMap
{
    public class GameMapMoveRespPayload
    {
        public static byte[] CreatePayload(GameMapAck ackCode, int posX, int posY)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            binaryWriter.Write(((int)(ackCode)));
            binaryWriter.Write(posX);
            binaryWriter.Write(posY);
            byteArray = memoryStream.ToArray();
            binaryWriter.Close();
            memoryStream.Close();
            return byteArray;
        }
        public static void ParsePayload(byte[] payload, out GameMapAck ackCode, out int posX, out int posY)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            ackCode = ((GameMapAck)(binaryReader.ReadInt32()));
            posX = binaryReader.ReadInt32();
            posY = binaryReader.ReadInt32();
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
