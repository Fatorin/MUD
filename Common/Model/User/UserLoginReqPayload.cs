using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.User
{
	public static class UserLoginReqPayload
	{
		public static byte[] CreatePayload(User infoData)
		{
			byte[] byteArray;
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
			if ((infoData != null))
			{
				binaryWriter.Write(true);
				UserLoginReqPayload.BinaryWriter(binaryWriter, infoData);
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

		public static void ParsePayload(byte[] payload, out User infoData)
		{
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
			System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
			if ((binaryReader.ReadBoolean() == true))
			{
				UserLoginReqPayload.BinaryReader(binaryReader, out infoData);
			}
			else
			{
				infoData = default(User);
			}
			binaryReader.Close();
			memoryStream.Close();
		}

		public static void BinaryWriter(System.IO.BinaryWriter binaryWriter, User obj)
		{
			if ((obj != null))
			{
				binaryWriter.Write(true);
				binaryWriter.Write(obj.UserId);
				binaryWriter.Write(obj.UserPwd);
			}
			else
			{
				binaryWriter.Write(false);
			}
		}
		public static void BinaryReader(System.IO.BinaryReader binaryReader, out User obj)
		{
			if ((binaryReader.ReadBoolean() == true))
			{
				obj = new User();
				obj.UserId = binaryReader.ReadString();
				obj.UserPwd = binaryReader.ReadString();
			}
			else
			{
				obj = default(User);
			}
		}
	}
}
