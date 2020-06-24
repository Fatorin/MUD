using Common;
using Common.Model.GameMap;
using Server.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Server.SeverSystem
{
    public class GameMapSystem : BaseSystem
    {
        public static GameMapSystem Instance { get; } = new GameMapSystem();
        public ConcurrentDictionary<long, GameMap> GameMapsDict = new ConcurrentDictionary<long, GameMap>();

        public GameMapSystem()
        {
            mappings.TryAdd((int)GameMapCommand.MoveReq, GameMapMoveReq);
            InitMaps();
        }

        public void InitMaps()
        {
            GameMap map1 = new GameMap(2331456, 5, 5);
            GameMapsDict.TryAdd(map1.GameMapUid, map1);
        }

        public void GameMapMoveReq(Player player,byte[] byteArray)
        {
            //讀取玩家REQ 回傳RESP
        }

        private bool isBorder(GameMap gameMap, int x, int y)
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
