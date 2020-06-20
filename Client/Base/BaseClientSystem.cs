using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Client.Base
{
    public class BaseClientSystem
    {
        public ConcurrentDictionary<int, Action<byte[]>> mappings;

        public BaseClientSystem()
        {
            mappings = new ConcurrentDictionary<int, Action<byte[]>>();
        }

        public virtual void PlayerEnter(int systemCommand, byte[] data)
        {
            //自行實作將資料接收的部分，並執行對應的內部function
            if (mappings.TryGetValue(systemCommand, out var function))
            {
                function(data);
            }
            else
            {
                Console.WriteLine($"invalid commnd={systemCommand}");
            }
        }
    }
}
