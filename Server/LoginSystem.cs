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
using System.Text;

namespace Server
{
    public sealed class LoginSystem : BaseSystem , IBaseRedisSystem<User>
    {
        //Singleton Mode
        public static LoginSystem Instance { get; } = new LoginSystem();

        public LoginSystem()
        {
            mappings.TryAdd((int)UserCommand.UserLoginReq,UserLoginReq);
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
                Send(player, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserRespLoginPayload.CreatePayload(UserAck.AuthFail)));
                return;
            }
            //驗證成功就通知另一個伺服器把人踢了(這邊要用Redis做)
            SockerManager.Instance.PublishLoginToRedis(infoData.UserId);
            //要重寫與另一個SERVER溝通的方法
            //回傳成功訊息給對應的人
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserRespLoginPayload.CreatePayload(UserAck.Success)));

            //回傳留言版最後一百筆資料
            MessageSystem.Instance.GetLastMessage(player);
            //新增玩家資料
        }

        public override void PlayerEnter(Player player, int command, byte[] bytesData)
        {
            base.PlayerEnter(player, command, bytesData);
        }

        public override void Send(Player player, byte[] byteData)
        {
            base.Send(player, byteData);
        }

        public override void Broadcast(byte[] byteData)
        {
            base.Broadcast(byteData);
        }

        //實作各Redis
        public string GetSystemRedisKey()
        {
            throw new NotImplementedException();
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, User InfoData)
        {
            throw new NotImplementedException();
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<User> InfoDatas)
        {
            throw new NotImplementedException();
        }

        public void SetExpiry(IDatabase redisDb)
        {
            throw new NotImplementedException();
        }
    }
}
