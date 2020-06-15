using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Base
{
    public interface IBaseRedisSystem<T>
    {
        public string GetSystemRedisKey();

        public void SaveOneInfoDataToRedis(IDatabase redisDb, T InfoData);

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<T> InfoDatas);

        public void SetExpiry(IDatabase redisDb);
    }
}
