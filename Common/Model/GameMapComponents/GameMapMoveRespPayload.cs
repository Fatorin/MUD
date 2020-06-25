using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.GameMapComponents
{
    public class GameMapMoveRespPayload
    {
        public static byte[] CreatePayload(GameMapAck ackCode, int posX, int posY, PlayerDataComponents.PlayerData.PlayerFaceEnum playerFace)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            binaryWriter.Write(((int)(ackCode)));
            binaryWriter.Write(posX);
            binaryWriter.Write(posY);
            binaryWriter.Write((int)playerFace);
            byteArray = memoryStream.ToArray();
            binaryWriter.Close();
            memoryStream.Close();
            return byteArray;
        }
        public static void ParsePayload(byte[] payload, out GameMapAck ackCode, out int posX, out int posY, out PlayerDataComponents.PlayerData.PlayerFaceEnum playerFace)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            ackCode = ((GameMapAck)(binaryReader.ReadInt32()));
            posX = binaryReader.ReadInt32();
            posY = binaryReader.ReadInt32();
            playerFace = (PlayerDataComponents.PlayerData.PlayerFaceEnum)binaryReader.ReadInt32();
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
