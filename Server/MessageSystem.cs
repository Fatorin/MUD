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
    public sealed class MessageSystem : ISystemHelper<Message>
    {
        //Singleton Mode
        public static MessageSystem Instance { get; } = new MessageSystem();

        private readonly string redisKey = "MessageList";
        public List<Message> GetLastMessage()
        {
            var values = GetRedisDb(RedisHelper.RedisDbNum.MsgData).ListRange(redisKey, -100, -1);
            var MsgInfoList = new List<Message>();
            foreach (string value in values)
            {
                MsgInfoList.Add(new Message { MessageString = value });
            }
            return MsgInfoList;
        }

        public IDatabase GetRedisDb(RedisHelper.RedisDbNum number)
        {
            return RedisHelper.Connection.GetDatabase((int)number);
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
