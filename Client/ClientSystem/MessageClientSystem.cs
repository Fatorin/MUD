using Client.Base;
using Common;
using Common.Model.Message;
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

        public override void PlayerEnter(int systemCommand, byte[] data)
        {
            base.PlayerEnter(systemCommand, data);
        }

        public void MessageReq(string data)
        {

        }

        public void MessageResp(byte[] data)
        {
            Program.mainUI.ShowLogOnResult("");
        }
    }
}
