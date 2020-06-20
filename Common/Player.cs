using Common.Model.PlayerData;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Player
    {
        public Socket Connection { get; }
        public DateTime LastLoginTime { get; }

        public PlayerData PlayerData { get; set; }
        public Player(Socket connection, int playerUid)
        {
            PlayerData = new PlayerData(playerUid);
            Connection = connection;
            LastLoginTime = DateTime.Now;
        }
    }
}
