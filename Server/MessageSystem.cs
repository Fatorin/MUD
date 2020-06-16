using Common;
using Common.Model.Command;
using Common.Model.Message;
using Newtonsoft.Json;
using Server.Base;
using Server.Redis;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public sealed class MessageSystem : BaseSystem, IBaseRedisSystem<Message>
    {
        //Singleton Mode
        public static MessageSystem Instance { get; } = new MessageSystem();

        public MessageSystem()
        {
            mappings.TryAdd((int)MessageCommand.MessageReq, MessageReq);
            //init mapping的程式
        }

        public void GetLastMessage(Player player)
        {
            var values = RedisHelper.GetRedisDb(RedisHelper.RedisDbNum.MsgData).ListRange(GetSystemRedisKey(), -100, -1);
            var MsgInfoList = new List<Message>();
            foreach (string value in values)
            {
                MsgInfoList.Add(new Message { MessageString = value });
            }
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.LoginSystem, (int)MessageCommand.MessageResp, MessageRespPayload.CreatePayload(MessageAck.Success, MsgInfoList.ToArray())));
        }

        private void MessageReq(Player player, byte[] byteArray)
        {
            MessageReqPayload.ParsePayload(byteArray, out var infoDatas);
            //存入Redis
            SaveOneInfoDataToRedis(RedisHelper.GetRedisDb(RedisHelper.RedisDbNum.MsgData), infoDatas[0]);
            //丟到Redis發布訊息(因為兩台同時註冊了，避免重送)
            SockerManager.Instance.PublishMessageToRedis(infoDatas[0].MessageString);
        }

        public void SendMsgToAll(Message[] infoDatas)
        {
            Broadcast(PacketBuilder.BuildPacket((int)SystemCategory.MessageSystem, (int)MessageCommand.MessageResp, MessageRespPayload.CreatePayload(MessageAck.Success, infoDatas.ToArray())));
        }

        //實作各Redis
        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<Message> infoDatas)
        {
            foreach (Message message in infoDatas)
            {
                redisDb.ListRightPush(GetSystemRedisKey(), message.MessageString);
            }
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, Message infoData)
        {
            redisDb.ListRightPush(GetSystemRedisKey(), JsonConvert.SerializeObject(infoData.MessageString));
        }

        public void SetExpiry(IDatabase redisDb)
        {
            redisDb.KeyExpire(GetSystemRedisKey(), TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay));
        }

        public string GetSystemRedisKey()
        {
            return nameof(Message);
        }
    }
}
