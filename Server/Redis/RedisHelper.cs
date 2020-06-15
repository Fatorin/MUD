using Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Redis
{
    public class RedisHelper
    {
        static RedisHelper()
        {
            var RedisGetConnectStr = GlobalSetting.RedisGetConnectString;
            _connection = new Lazy<ConnectionMultiplexer>(() =>
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

        public static IDatabase GetRedisDb(RedisHelper.RedisDbNum number)
        {
            return Connection.GetDatabase((int)number);
        }

        public enum RedisDbNum
        {
            Connect,
            MsgData,
        }
    }
}
