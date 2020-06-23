using Client.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    class GameEventClientSystem : BaseClientSystem
    {
        public static GameEventClientSystem Instance { get; } = new GameEventClientSystem();

        public GameEventClientSystem()
        {

        }
    }
}
