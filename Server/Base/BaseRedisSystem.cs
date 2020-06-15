using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Base
{
    public class BaseRedisSystem<T>
    {
        public readonly string redisKey = nameof(T);

        public virtual void SaveOneInfoDataToRedis(IDatabase redisDb, T infoData)
        {
            //實作存取單個Redis物件
        }

        public virtual void SaveMultiInfoDataToRedis(IDatabase redisDb, List<T> infoDatas)
        {
            //實作存取多個Redis物件
        }

        public virtual void SetExpiry(IDatabase redisDb)
        {
            //實作REDIS物件的期限
        }
    }
}
