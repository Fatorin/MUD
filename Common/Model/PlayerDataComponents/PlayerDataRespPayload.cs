using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Common.Model.PlayerDataComponents
{
    public class PlayerDataRespPayload
    {
        public static byte[] CreatePayload(PlayerDataAck ackCode, PlayerData infoData)
        {
            byte[] dataArray;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write((int)ackCode);
            binaryFormatter.Serialize(memoryStream, infoData);
            binaryWriter.Close();
            memoryStream.Close();
            dataArray = memoryStream.ToArray();
            return dataArray;
        }

        public static void ParsePayload(byte[] payload, out PlayerDataAck ackCode, out PlayerData infoData)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(payload);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            ackCode = (PlayerDataAck)binaryReader.ReadInt32();
            infoData = (PlayerData)binaryFormatter.Deserialize(memoryStream);
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
