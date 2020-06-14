using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Player
    {
        public string PlayerId { get; }
        public Socket Connection { get; }
        public string PlayerSession { get; }
        public DateTime LastLoginTime { get; }
        public Player(string playerId, Socket connection)
        {
            PlayerId = playerId;
            Connection = connection;
            PlayerSession = SessionCreate();
            LastLoginTime = DateTime.Now;
        }

        private string SessionCreate()
        {
            //想一套公式傳給CLIENT
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(PlayerId);
            return "";
        }
    }
}
