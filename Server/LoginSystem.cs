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
        public string GetRedisDataKey()
        {
            throw new NotImplementedException();
        }

        public IDatabase GetRedisDb(Helper.RedisDbNum number)
        {
            throw new NotImplementedException();
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, string key, List<User> infoDatas)
        {
            throw new NotImplementedException();
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, User infoData)
        {
            throw new NotImplementedException();
        }

        public void SetExpiry(IDatabase redisDb, string key)
        {
            throw new NotImplementedException();
        }

        IEnumerator<User> IEnumerable<User>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
