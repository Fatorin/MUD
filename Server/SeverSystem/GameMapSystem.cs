using Common;
using Common.Model.GameMap;
using Server.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.SeverSystem
{
    public class GameMapSystem : BaseSystem
    {
        public static GameMapSystem Instance { get; } = new GameMapSystem();

        public GameMapSystem()
        {

        }

        public void GameMapMoveReq(Player player,byte[] byteArray)
        {

        }

        private bool CheckBorder(GameMap gameMap, int x, int y)
        {
            if(x > gameMap.MapSizeX || x< 0)
            {
                return false;
            }

            if(y > gameMap.MapSizeY || y< 0)
            {
                return false;
            }

            return true;
        }
    }
}
