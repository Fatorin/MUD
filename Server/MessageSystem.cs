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
        private readonly string redisKey = "MessageList";

        public List<Message> GetLastMessage()
        {
            var values = GetRedisDb(Helper.RedisDbNum.MsgData).ListRange(redisKey, -100, -1);
            var MsgInfoList = new List<Message>();
            foreach (string value in values)
            {
                MsgInfoList.Add(new Message { MessageString = value });
            }
            return MsgInfoList;
        }

        public IDatabase GetRedisDb(Helper.RedisDbNum number)
        {
            return Helper.Connection.GetDatabase((int)number);
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<Message> infoDatas)
        {
            foreach (Message message in infoDatas)
            {
                redisDb.ListRightPush(redisKey, message.MessageString);
            }
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, Message infoData)
        {
            redisDb.ListRightPush(redisKey, JsonConvert.SerializeObject(infoData.MessageString));
        }

        public void SetExpiry(IDatabase redisDb)
        {
            redisDb.KeyExpire(redisKey, TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay));
        }

    }
}
