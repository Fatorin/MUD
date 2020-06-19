using Client.Base;
using Common;
using Common.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class LoginClientSystem : BaseClientSystem
    {
        public static LoginClientSystem Instance { get; } = new LoginClientSystem();

        public LoginClientSystem()
        {
            mappings.TryAdd((int)UserCommand.UserLoginResp, LoginResp);
        }

        public override void PlayerEnter(int systemCommand, byte[] data)
        {
            base.PlayerEnter(systemCommand, data);
        }

        public void LoginReq(string name, string password)
        {
            //沒實作，因為已經在前一個部分就先使用了
        }

        public void LoginResp(byte[] data)
        {
            Program.mainUI.ShowLogOnResult("");
        }
    }
}
