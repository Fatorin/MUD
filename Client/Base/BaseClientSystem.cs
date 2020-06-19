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
            //各自實作
        }
    }
}
