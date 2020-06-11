using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.Message
{
    public static class MessageReqPayload
    {
        public static byte[] CreatePayload(Message[] infoDataArray)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            if ((infoDataArray != null))
            {
                binaryWriter.Write(true);
                binaryWriter.Write(infoDataArray.Length);
                for (int i0 = 0; (i0 < infoDataArray.Length); i0 = (i0 + 1))
                {
                    if ((infoDataArray[i0] != null))
                    {
                        binaryWriter.Write(true);
                        MessageReqPayload.BinaryWriter(binaryWriter, infoDataArray[i0]);
                    }
                    else
                    {
                        binaryWriter.Write(false);
                    }
                }
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

        public static void ParsePayload(byte[] payload, out Message[] infoDataArray)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            if (binaryReader.ReadBoolean() == true)
            {
                infoDataArray = new Message[binaryReader.ReadInt32()];
                for (int i0 = 0; (i0 < infoDataArray.Length); i0 = (i0 + 1))
                {
                    if ((binaryReader.ReadBoolean() == true))
                    {
                        MessageReqPayload.BinaryReader(binaryReader, out infoDataArray[i0]);
                    }
                    else
                    {
                        infoDataArray[i0] = default(Message);
                    }
                }
            }
            else
            {
                infoDataArray = default(Message[]);
            }
            binaryReader.Close();
            memoryStream.Close();
        }

        private static void BinaryWriter(System.IO.BinaryWriter binaryWriter, Message obj)
        {
            if ((obj != null))
            {
                binaryWriter.Write(true);
                binaryWriter.Write(obj.MessageString);
            }
            else
            {
                binaryWriter.Write(false);
            }
        }
        private static void BinaryReader(System.IO.BinaryReader binaryReader, out Message obj)
        {
            if ((binaryReader.ReadBoolean() == true))
            {
                obj = new Message();
                obj.MessageString = binaryReader.ReadString();
            }
            else
            {
                obj = default(Message);
            }
        }
    }
}
