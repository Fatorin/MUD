using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Player
    {
        public int PlayerUid { get; }

        public Socket Connection { get; }
        public DateTime LastLoginTime { get; }
        public Player(int playerUid, Socket connection)
        {
            PlayerUid = playerUid;
            Connection = connection;
            LastLoginTime = DateTime.Now;
        }

        public void SetPlayerUid()
        {

        }
    }
}
