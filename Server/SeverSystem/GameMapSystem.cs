using Common;
using Common.Model.Command;
using Common.Model.GameMapComponents;
using Common.Model.PlayerDataComponents;
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
        public readonly long InitMapSeed = 2331456;
        private ConcurrentDictionary<PlayerData.PlayerFaceEnum, ConcurrentDictionary<GameMapAction.MoveAction, int[]>> _PlayerMovePosDict;

        public GameMapSystem()
        {
            mappings.TryAdd((int)GameMapCommand.MoveReq, GameMapMoveReq);
            InitMaps();
            InitPlayerMovePos();
        }

        public void InitMaps()
        {
            GameMap map1 = new GameMap(InitMapSeed, 5, 5);
            GameMapsDict.TryAdd(map1.GameMapUid, map1);
        }

        public void InitPlayerMovePos()
        {
            //依據面向設定綁定對應的行動後產出的X、Y、面向值
            _PlayerMovePosDict = new ConcurrentDictionary<PlayerData.PlayerFaceEnum, ConcurrentDictionary<GameMapAction.MoveAction, int[]>>();
            var NorthActionDict = new ConcurrentDictionary<GameMapAction.MoveAction, int[]>();
            NorthActionDict.TryAdd(GameMapAction.MoveAction.GoStraight, new int[] { 0, 1, (int)PlayerData.PlayerFaceEnum.North });
            NorthActionDict.TryAdd(GameMapAction.MoveAction.TurnLeft, new int[] { -1, 0, (int)PlayerData.PlayerFaceEnum.West });
            NorthActionDict.TryAdd(GameMapAction.MoveAction.TurnRight, new int[] { 1, 0, (int)PlayerData.PlayerFaceEnum.East });
            NorthActionDict.TryAdd(GameMapAction.MoveAction.GoBackward, new int[] { 0, -1, (int)PlayerData.PlayerFaceEnum.South });
            var WestActionDict = new ConcurrentDictionary<GameMapAction.MoveAction, int[]>();
            WestActionDict.TryAdd(GameMapAction.MoveAction.GoStraight, new int[] { -1, 0, (int)PlayerData.PlayerFaceEnum.West });
            WestActionDict.TryAdd(GameMapAction.MoveAction.TurnLeft, new int[] { 0, -1, (int)PlayerData.PlayerFaceEnum.South });
            WestActionDict.TryAdd(GameMapAction.MoveAction.TurnRight, new int[] { 0, 1, (int)PlayerData.PlayerFaceEnum.North });
            WestActionDict.TryAdd(GameMapAction.MoveAction.GoBackward, new int[] { 1, 0, (int)PlayerData.PlayerFaceEnum.East });
            var EastActionDict = new ConcurrentDictionary<GameMapAction.MoveAction, int[]>();
            EastActionDict.TryAdd(GameMapAction.MoveAction.GoStraight, new int[] { 1, 0, (int)PlayerData.PlayerFaceEnum.East });
            EastActionDict.TryAdd(GameMapAction.MoveAction.TurnLeft, new int[] { 0, 1, (int)PlayerData.PlayerFaceEnum.North });
            EastActionDict.TryAdd(GameMapAction.MoveAction.TurnRight, new int[] { 0, -1, (int)PlayerData.PlayerFaceEnum.South });
            EastActionDict.TryAdd(GameMapAction.MoveAction.GoBackward, new int[] { -1, 0, (int)PlayerData.PlayerFaceEnum.West });
            var SouthActionDict = new ConcurrentDictionary<GameMapAction.MoveAction, int[]>();
            SouthActionDict.TryAdd(GameMapAction.MoveAction.GoStraight, new int[] { 0, -1, (int)PlayerData.PlayerFaceEnum.South });
            SouthActionDict.TryAdd(GameMapAction.MoveAction.TurnLeft, new int[] { 1, 0, (int)PlayerData.PlayerFaceEnum.East });
            SouthActionDict.TryAdd(GameMapAction.MoveAction.TurnRight, new int[] { -1, 0, (int)PlayerData.PlayerFaceEnum.West });
            SouthActionDict.TryAdd(GameMapAction.MoveAction.GoBackward, new int[] { 0, 1, (int)PlayerData.PlayerFaceEnum.North });

            _PlayerMovePosDict.TryAdd(PlayerData.PlayerFaceEnum.North, NorthActionDict);
            _PlayerMovePosDict.TryAdd(PlayerData.PlayerFaceEnum.West, WestActionDict);
            _PlayerMovePosDict.TryAdd(PlayerData.PlayerFaceEnum.East, EastActionDict);
            _PlayerMovePosDict.TryAdd(PlayerData.PlayerFaceEnum.South, SouthActionDict);
        }

        public void GameMapMoveReq(Player player, byte[] byteArray)
        {
            GameMapMoveReqPayload.ParsePayload(byteArray, out var moveAction);

            var ackCode = GameMapAck.Success;
            if (!GameMapsDict.TryGetValue(player.PlayerData.MapSeed, out var gameMap))
            {
                Console.WriteLine("不存在的地圖");
                ackCode = GameMapAck.NoExistMapSeed;
            }

            //檢查面向，依照面向的位置往前後左右移動，然後再計算有沒有超界
            if (!isBorder(gameMap, player, moveAction))
            {
                Console.WriteLine("超越地圖邊界、不存在的動作");
                ackCode = GameMapAck.OverBorder;
            }

            var payload = GameMapMoveRespPayload.CreatePayload(ackCode, player.PlayerData.PosX, player.PlayerData.PosY, player.PlayerData.PlayeyFace);
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.GameMapSystem, (int)GameMapCommand.MoveResp, payload));
        }

        private bool isBorder(GameMap gameMap, Player player, GameMapAction.MoveAction action)
        {
            //抓取對應的面向 理論上不可能抓不到
            if (!_PlayerMovePosDict.TryGetValue(player.PlayerData.PlayeyFace, out var directionDict))
            {
                return false;
            }

            //抓取對應的面向行動後的POS值增減 理論上不可能抓不到
            if (!directionDict.TryGetValue(action, out var posArray))
            {
                return false;
            }

            var posX = posArray[0];
            var posY = posArray[1];
            var playerFace = (PlayerData.PlayerFaceEnum)posArray[2];

            //計算會不會超出地圖邊界
            if (player.PlayerData.PosX + posX > gameMap.MapSizeX || player.PlayerData.PosX + posX < 0)
            {
                return false;
            }

            if (player.PlayerData.PosY + posY > gameMap.MapSizeY || player.PlayerData.PosY + posY < 0)
            {
                return false;
            }

            //更新該玩家於地圖上的位置
            player.PlayerData.PosX += posX;
            player.PlayerData.PosY += posY;
            player.PlayerData.PlayeyFace = playerFace;
            return true;
        }
    }
}
