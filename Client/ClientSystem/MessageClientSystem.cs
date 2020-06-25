using Client.Base;
using Common;
using Common.Model.MessageComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class MessageClientSystem : BaseClientSystem
    {
        public static MessageClientSystem Instance { get; } = new MessageClientSystem();

        public MessageClientSystem()
        {
            mappings.TryAdd((int)MessageCommand.MessageResp, MessageResp);
        }

        public void MessageReq(string data)
        {

        }

        public void MessageResp(byte[] data)
        {
            MessageRespPayload.ParsePayload(data, out var ackCode, out var messageInfos);
            Program.mainUI.ShowLogOnResult($"ackCode={ackCode}");
            foreach(Message message in messageInfos)
            {
                Program.mainUI.ShowLogOnResult($"message={message.MessageString}");
            }
        }
    }
}
