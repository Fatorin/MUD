using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Redis
{
    interface ISystemHelper<T>
    {
        public IDatabase GetRedisDb(RedisHelper.RedisDbNum number);
        public void SaveOneInfoDataToRedis(IDatabase redisDb, T infoData);
        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<T> infoDatas);
        public void SetExpiry(IDatabase redisDb);
    }
}
