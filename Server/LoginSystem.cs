using Common;
using Common.Model.User;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public sealed class LoginSystem : ISystemHelper<User>
    {
        //Singleton Mode
        public static LoginSystem Instance { get; } = new LoginSystem();

        private readonly string redisKey = "LoginList";

        public IDatabase GetRedisDb(RedisHelper.RedisDbNum number)
        {
            return RedisHelper.Connection.GetDatabase((int)number);
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<User> infoDatas)
        {
            throw new NotImplementedException();
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, User infoData)
        {
            throw new NotImplementedException();
        }

        public void SetExpiry(IDatabase redisDb)
        {
            redisDb.KeyExpire(redisKey, TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay));
        }

    }
}
