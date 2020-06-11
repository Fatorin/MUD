using Common;
using Common.Model.Message;
using Newtonsoft.Json;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MessageSystem : IHelper<Message>
    {
        public IEnumerator<Message> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IDatabase GetRedisDb(Helper.RedisDbNum number)
        {
            return Helper.Connection.GetDatabase((int)number);
        }

        public string GetRedisDataKey()
        {
            return "MessageList";
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, string key, List<Message> infoDatas)
        {
            foreach(Message message in infoDatas)
            {
                redisDb.ListRightPush(key, message.MessageString);
            }
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, Message infoData)
        {
            redisDb.ListRightPush(GetRedisDataKey(), JsonConvert.SerializeObject(infoData.MessageString));
        }

        public void SetExpiry(IDatabase redisDb, string key)
        {
            redisDb.KeyExpire(key, TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
