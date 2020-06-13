using Common.Model.User;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class LoginSystem : IHelper<User>
    {
        private readonly string redisKey = "MessageList";

        public IDatabase GetRedisDb(Helper.RedisDbNum number)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}
