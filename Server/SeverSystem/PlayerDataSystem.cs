using Common;
using Common.Model.PlayerData;
using Common.Model.User;
using Newtonsoft.Json;
using Server.Base;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ServerSystem
{
    public class PlayerDataSystem : BaseSystem, IBaseRedisSystem<PlayerData>
    {
        public static PlayerDataSystem Instance { get; } = new PlayerDataSystem();

        public PlayerDataSystem()
        {

        }

        public string GetSystemRedisKey()
        {
            return nameof(PlayerData);
        }

        public PlayerData GetOneInfoDataFromRedis(IDatabase redisDb, int playerUid)
        {
            var value = redisDb.HashGet(GetSystemRedisKey(), playerUid);
            return JsonConvert.DeserializeObject<PlayerData>(value);
        }

        public void SaveOneInfoDataToRedis(IDatabase redisDb, PlayerData infoData)
        {
            redisDb.HashSet(GetSystemRedisKey(), infoData.PlayerUid, JsonConvert.SerializeObject(infoData));
        }

        public void SaveMultiInfoDataToRedis(IDatabase redisDb, List<PlayerData> infoDatas)
        {
            var hashes = new List<HashEntry>();

            infoDatas.ForEach(element =>
            {
                hashes.Add(new HashEntry(element.PlayerUid, JsonConvert.SerializeObject(element)));
            });

            redisDb.HashSet(GetSystemRedisKey(), hashes.ToArray());
        }

        public void SetExpiry(IDatabase redisDb)
        {
            redisDb.KeyExpire(GetSystemRedisKey(), TimeSpan.FromDays(GlobalSetting.RedisKeyExpireNormalDay));
        }
    }
}
