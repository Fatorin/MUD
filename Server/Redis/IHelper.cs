using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Redis
{
    interface IHelper<T> : IEnumerable<T>
    {
        public IDatabase GetRedisDb(Helper.RedisDbNum number);
        public string GetRedisDataKey();
        public void SaveOneInfoDataToRedis(IDatabase redisDb, T infoData);
        public void SaveMultiInfoDataToRedis(IDatabase redisDb, string key, List<T> infoDatas);
        public void SetExpiry(IDatabase redisDb, string key);
    }
}
