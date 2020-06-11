using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class GlobalSetting
    {
        public static string LocalIP = "127.0.0.1";
        public static int PortNum1 = 9987;
        public static int PortNum2 = 9988;
        public static readonly string RedisGetConnectString = $"{LocalIP}:16800,password=jfiredis";
        public static double RedisKeyExpireNormalDay = 1;
    }
}
