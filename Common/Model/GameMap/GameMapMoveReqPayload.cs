using Common.Model.Message;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Model.PlayerData.PlayerData;

namespace Common.Model.GameMap
{
    public class GameMapMoveReqPayload
    {
        public static byte[] CreatePayload(int posX, int posY, PlayerFaceEnum playerFace)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            binaryWriter.Write(posX);
            binaryWriter.Write(posY);
            binaryWriter.Write((int)playerFace);
            byteArray = memoryStream.ToArray();
            binaryWriter.Close();
            memoryStream.Close();
            return byteArray;
        }

        public static void ParsePayload(byte[] payload, out int posX, out int posY, out PlayerFaceEnum playerFace)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            if ((binaryReader.ReadBoolean() == true))
            {
                posX = binaryReader.ReadInt32();
                posY = binaryReader.ReadInt32();
                playerFace = (PlayerFaceEnum)binaryReader.ReadInt32();
            }
            else
            {
                posX = -1;
                posY = -1;
                playerFace = PlayerFaceEnum.Empty;
            }
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
