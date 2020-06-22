using Client.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class GameMapClientSystem : BaseClientSystem
    {
        public static GameMapClientSystem Instance { get; } = new GameMapClientSystem();
    
        public GameMapClientSystem()
        {

        }

        private void OnGameEventResp(byte[] data)
        {

        }
    }
}
