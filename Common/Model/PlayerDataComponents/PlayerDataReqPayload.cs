using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.PlayerDataComponents
{
    public class PlayerDataReqPayload
    {
		public static byte[] CreatePayload(int playerId)
		{
			byte[] byteArray;
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
			if ((playerId != 0))
			{
				binaryWriter.Write(true);
				PlayerDataReqPayload.BinaryWriter(binaryWriter, playerId);
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

		public static void ParsePayload(byte[] payload, out int playerId)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
			System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
			if ((binaryReader.ReadBoolean() == true))
			{
				PlayerDataReqPayload.BinaryReader(binaryReader, out playerId);
			}
			else
			{
				playerId = 0;
			}
			binaryReader.Close();
			memoryStream.Close();
		}

		public static void BinaryWriter(System.IO.BinaryWriter binaryWriter, int obj)
		{
			if ((obj != 0))
			{
				binaryWriter.Write(true);
				binaryWriter.Write(obj);
			}
			else
			{
				binaryWriter.Write(false);
			}
		}
		public static void BinaryReader(System.IO.BinaryReader binaryReader, out int obj)
		{
			if ((binaryReader.ReadBoolean() == true))
			{
				obj = binaryReader.ReadInt32();
			}
			else
			{
				obj = 0;
			}
		}
	}
}
