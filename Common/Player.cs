using Common.Model.Player;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Player : PlayerData
    {
        public Socket Connection { get; }
        public DateTime LastLoginTime { get; }
        public Player(Socket connection)
        {
            Connection = connection;
            LastLoginTime = DateTime.Now;
        }

    }
}
