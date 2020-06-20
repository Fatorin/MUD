using Common;
using Common.Model.Command;
using Common.Model.Message;
using Common.Model.PlayerData;
using Common.Model.User;
using Newtonsoft.Json;
using Server.Base;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Server
{
    public sealed class LoginSystem : BaseSystem, IBaseRedisSystem<PlayerData>
    {
        //Singleton Mode
        public static LoginSystem Instance { get; } = new LoginSystem();

        public LoginSystem()
        {
            mappings.TryAdd((int)UserCommand.UserLoginReq, UserLoginReq);
        }

        private void UserLoginReq(Player player, byte[] byteArray)
        {
            UserLoginReqPayload.ParsePayload(byteArray, out var infoData);
            var dbContext = SockerManager.Instance.GetApplicationContext();
            //SingleOrDefault will return null if no exist.
            var user = dbContext.Users.Where(u => u.UserId == infoData.UserId).SingleOrDefault();
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
                user = dbContext.Users.Where(u => u.UserId == infoData.UserId).SingleOrDefault();
                //更新該玩家的資料
                player.PlayerData.PlayerUid = user.PlayerUid;
                //未來會做成存在DB或REDIS
            }

            //建立完帳戶、確認用戶帳密是否一致
            //不一致就傳送失敗訊號，並且剔除使用者
            if (infoData.UserPwd != user.UserPwd)
            {
                Send(player, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserLoginRespPayload.CreatePayload(UserAck.AuthFail)));
                return;
            }

            //更新Player資料 從DB重撈
            player.PlayerData = GetOneInfoDataFromRedis(RedisHelper.GetRedisDb(RedisHelper.RedisDbNum.Connect), user.PlayerUid);

            //驗證成功就通知在線上的伺服器，把人踢下線
            SockerManager.Instance.PublishLoginToRedis(user.PlayerUid);
            //回傳成功訊息給對應的人
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)UserCommand.UserLoginResp, UserLoginRespPayload.CreatePayload(UserAck.Success)));
            //傳送玩家資料給他
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.PlayerSystem, (int)PlayerDataCommand.PlayerDataResp, PlayerDataRespPayload.CreatePayload(PlayerDataAck.Success, player.PlayerData)));
            //儲存玩家資料進Redis
            SaveOneInfoDataToRedis(RedisHelper.GetRedisDb(RedisHelper.RedisDbNum.Connect), player.PlayerData);
            //回傳留言版最後一百筆資料
            MessageSystem.Instance.SendLastMessage(player);
            //更新玩家資料
            SockerManager.Instance.SavePlayerConnect(player.Connection.RemoteEndPoint.ToString(), player);
        }

        public string GetSystemRedisKey()
        {
            return nameof(PlayerData);
        }

        public PlayerData GetOneInfoDataFromRedis(IDatabase redisDb, int playerUid)
        {
            var value = redisDb.HashGet(GetSystemRedisKey(), playerUid);
            return JsonConvert.DeserializeObject<PlayerData>(value);
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, PlayerData infoData)
        {
            redisDb.HashSet(GetSystemRedisKey(), infoData.PlayerUid, JsonConvert.SerializeObject(infoData));
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<PlayerData> infoDatas)
        {
            var hashes = new List<HashEntry>();

            infoDatas.ForEach(element =>
            {
                hashes.Add(new HashEntry(element.PlayerUid, JsonConvert.SerializeObject(element)));
            });

            redisDb.HashSet(GetSystemRedisKey(), hashes.ToArray());
        }

        public void SetExpiry(IDatabase redisDb)
        {
            redisDb.KeyExpire(GetSystemRedisKey(), TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay));
        }
    }
}
