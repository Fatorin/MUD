using Common.Model.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.GameMap
{
    public class GameMapMoveReqPayload
    {
        public static byte[] CreatePayload(int posX, int posY)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            if (posX >= 0 || posY >= 0)
            {
                binaryWriter.Write(true);
                binaryWriter.Write(posX);
                binaryWriter.Write(posY);
            }
            else
            {
                binaryWriter.Write(false);
            }
            byteArray = memoryStream.ToArray();
            binaryWriter.Close();
            memoryStream.Close();
            return byteArray;
        }

        public static void ParsePayload(byte[] payload, out int posX, out int posY)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            if ((binaryReader.ReadBoolean() == true))
            {
                posX = binaryReader.ReadInt32();
                posY = binaryReader.ReadInt32();
            }
            else
            {
                posX = -1;
                posY = -1;
            }
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
