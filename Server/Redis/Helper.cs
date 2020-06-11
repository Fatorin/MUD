using Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Redis
{
    class Helper
    {
        static Helper()
        {
            var RedisGetConnectStr = GlobalSetting.GetRedisGetConnectStr();
            Helper._connection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(RedisGetConnectStr);
            });
        }

        private static Lazy<ConnectionMultiplexer> _connection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _connection.Value;
            }
        }

        public enum RedisLinkNumber
        {
            Connect,
            MsgData,
        }
    }
}
