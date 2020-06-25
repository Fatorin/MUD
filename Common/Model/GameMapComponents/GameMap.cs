using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.GameMapComponents
{
    public class GameMap
    {
        public long GameMapUid { get; }
        public int MapSizeX { get; }
        public int MapSizeY { get; }
        public int[,] GameMapSize { get; }

        public GameMap(long gameMapUid, int mapSizeX, int mapSizeY)
        {
            GameMapUid = gameMapUid;
            MapSizeX = mapSizeX;
            MapSizeY = mapSizeY;
            GameMapSize = new int[mapSizeX, mapSizeY];
        }
    }
}
