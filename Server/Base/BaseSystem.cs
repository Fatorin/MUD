using Common;
using Common.Model.User;
using Server.Base;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Server.Redis
{
    public class BaseSystem
    {
        public ConcurrentDictionary<int,Action<Player,byte[]>> mappings;

        public BaseSystem()
        {
            mappings = new ConcurrentDictionary<int, Action<Player, byte[]>>();
        }
        public virtual void PlayerEnter(Player player, int command, byte[] bytesData)
        {
            //自行實作將資料接收的部分，並執行對應的內部function
            if (mappings.TryGetValue(command, out var function))
            {
                function(player, bytesData);
            }
            else
            {
                Console.WriteLine($"invalid commnd={command}");
            }
        }

        public virtual void Send(Player player, byte[] byteData)
        {
            //實作傳送單一物件給玩家
            SockerManager.Instance.Send(player, byteData);
        }
        public virtual void Broadcast(byte[] byteData)
        {
            //實作廣播給所有用戶
            SockerManager.Instance.Broadcast(byteData);
        }
    }
}
