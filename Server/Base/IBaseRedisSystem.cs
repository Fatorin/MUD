using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Base
{
    public interface IBaseRedisSystem<T>
    {
        public string GetSystemRedisKey();

        public T GetOneInfoDataFromRedis(IDatabase redisDb, int playerUid);
        
        public void SaveOneInfoDataToRedis(IDatabase redisDb, T InfoData);

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<T> infoDatas);

        public void SetExpiry(IDatabase redisDb);
    }
}
