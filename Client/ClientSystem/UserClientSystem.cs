using Client.Base;
using Common;
using Common.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class UserClientSystem : BaseClientSystem
    {
        public static UserClientSystem Instance { get; } = new UserClientSystem();

        public UserClientSystem()
        {
            mappings.TryAdd((int)UserCommand.UserLoginResp, LoginResp);
        }

        public void LoginReq(string name, string password)
        {
            //沒實作，因為已經在前一個部分就先使用了
        }

        public void LoginResp(byte[] data)
        {
            UserLoginRespPayload.ParsePayload(data, out UserAck ack);
            Program.mainUI.ShowLogOnResult($"ack={ack}");
        }
    }
}
