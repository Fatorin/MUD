using Common;
using Common.Model.Command;
using Common.Model.Message;
using Common.Model.User;
using Server.Base;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Server
{
    public sealed class LoginSystem : BaseSystem
    {
        //Singleton Mode
        public static LoginSystem Instance { get; } = new LoginSystem();

        public LoginSystem()
        {
            mappings.TryAdd((int)UserCommand.UserLoginReq, UserLoginReq);
        }

        private void UserLoginReq(Player player, byte[] byteArray)
        {
            UserReqLoginPayload.ParsePayload(byteArray, out var infoData);
            var dbContext = SockerManager.Instance.GetApplicationContext();
            var user = dbContext.Users.Find(infoData.UserId);
            //如果用戶不存在則自動幫他創帳號
            if (user == null)
            {
                Console.WriteLine("not found equal userid, created new user.");
                user = new User
                {
                    UserId = infoData.UserId,
                    UserPwd = infoData.UserPwd
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            //建立完帳戶、確認用戶帳密是否一致
            //不一致就傳送失敗訊號，並且剔除使用者
            if (infoData.UserPwd != user.UserPwd)
            {
                Send(player, PacketBuilder.BuildPacket(0, (int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserRespLoginPayload.CreatePayload(UserAck.AuthFail)));
                return;
            }
            var playerUid = dbContext.Users.Find(infoData.UserId).PlayerUid;
            player = new Player(playerUid, player.Connection);

            //驗證成功就通知另一個伺服器把人踢了(這邊要用Redis做)
            SockerManager.Instance.PublishLoginToRedis(infoData.PlayerUid);
            //要重寫與另一個SERVER溝通的方法
            //回傳成功訊息給對應的人
            Send(player, PacketBuilder.BuildPacket(player.PlayerUid, (int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserRespLoginPayload.CreatePayload(UserAck.Success)));

            //回傳留言版最後一百筆資料
            MessageSystem.Instance.GetLastMessage(player);
            //新增玩家資料
            SockerManager.Instance.SavePlayer(player);
        }

        public override void PlayerEnter(Player player, int command, byte[] bytesData)
        {
            if (mappings.TryGetValue(command, out var function))
            {
                function(player, bytesData);
            }
            else
            {
                Console.WriteLine($"invalid commnd={command}");
            }
        }
    }
}
