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

        public void GameMapMoveReq(Player player, byte[] byteArray)
        {
            GameMapMoveReqPayload.ParsePayload(byteArray, out var moveAction);

            var ackCode = GameMapAck.Unknown;
            if (GameMapsDict.TryGetValue(player.PlayerData.MapSeed, out var gameMap))
            {
                Console.WriteLine("不存在的地圖");
                ackCode = GameMapAck.NoExistMapSeed;
            }

            //檢查面向，依照面向的位置往前後左右移動，然後再計算有沒有超界
            if (isBorder(gameMap, player, moveAction))
            {
                Console.WriteLine("超越地圖邊界了");
                ackCode = GameMapAck.OverBorder;
            }

            ackCode = GameMapAck.Success;
            var payload = GameMapMoveRespPayload.CreatePayload(ackCode, player.PlayerData.PosX, player.PlayerData.PosY, player.PlayerData.PlayeyFace);
            Send(player, PacketBuilder.BuildPacket((int)SystemCategory.GameMapSystem, (int)GameMapCommand.MoveResp, payload));
        }

        private bool isBorder(GameMap gameMap, Player player, GameMapAction.MoveAction action)
        {
            var posX = 0;
            var posY = 0;
            var playerFace = PlayerData.PlayerFaceEnum.North;

            //行動後的XY值
            switch (player.PlayerData.PlayeyFace)
            {
                case PlayerData.PlayerFaceEnum.North:
                    NorthAction(action, out posX, out posY, out playerFace);
                    break;
                case PlayerData.PlayerFaceEnum.West:
                    WestAction(action, out posX, out posY, out playerFace);
                    break;
                case PlayerData.PlayerFaceEnum.East:
                    EastAction(action, out posX, out posY, out playerFace);
                    break;
                case PlayerData.PlayerFaceEnum.South:
                    SouthAction(action, out posX, out posY, out playerFace);
                    break;
            }

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

        private void NorthAction(GameMapAction.MoveAction action, out int PosX, out int PosY, out PlayerData.PlayerFaceEnum playerFace)
        {
            PosX = 0;
            PosY = 0;
            playerFace = PlayerData.PlayerFaceEnum.North;

            switch (action)
            {
                case GameMapAction.MoveAction.GoStraight:
                    PosX = 0;
                    PosY = 1;
                    playerFace = PlayerData.PlayerFaceEnum.North;
                    break;
                case GameMapAction.MoveAction.TurnLeft:
                    PosX = -1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.West;
                    break;
                case GameMapAction.MoveAction.TurnRight:
                    PosX = 1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.East;
                    break;
                case GameMapAction.MoveAction.GoBackward:
                    PosX = 0;
                    PosY = -1;
                    playerFace = PlayerData.PlayerFaceEnum.South;
                    break;
            }
        }

        private void WestAction(GameMapAction.MoveAction action, out int PosX, out int PosY, out PlayerData.PlayerFaceEnum playerFace)
        {
            PosX = 0;
            PosY = 0;
            playerFace = PlayerData.PlayerFaceEnum.North;

            switch (action)
            {
                case GameMapAction.MoveAction.GoStraight:
                    PosX = -1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.West;
                    break;
                case GameMapAction.MoveAction.TurnLeft:
                    PosX = 0;
                    PosY = -1;
                    playerFace = PlayerData.PlayerFaceEnum.South;
                    break;
                case GameMapAction.MoveAction.TurnRight:
                    PosX = 0;
                    PosY = 1;
                    playerFace = PlayerData.PlayerFaceEnum.North;
                    break;
                case GameMapAction.MoveAction.GoBackward:
                    PosX = 1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.East;
                    break;
            }
        }

        private void EastAction(GameMapAction.MoveAction action, out int PosX, out int PosY, out PlayerData.PlayerFaceEnum playerFace)
        {
            PosX = 0;
            PosY = 0;
            playerFace = PlayerData.PlayerFaceEnum.North;

            switch (action)
            {
                case GameMapAction.MoveAction.GoStraight:
                    PosX = 1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.East;
                    break;
                case GameMapAction.MoveAction.TurnLeft:
                    PosX = 0;
                    PosY = 1;
                    playerFace = PlayerData.PlayerFaceEnum.North;
                    break;
                case GameMapAction.MoveAction.TurnRight:
                    PosX = 0;
                    PosY = -1;
                    playerFace = PlayerData.PlayerFaceEnum.South;
                    break;
                case GameMapAction.MoveAction.GoBackward:
                    PosX = -1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.West;
                    break;
            }
        }

        private void SouthAction(GameMapAction.MoveAction action, out int PosX, out int PosY, out PlayerData.PlayerFaceEnum playerFace)
        {
            PosX = 0;
            PosY = 0;
            playerFace = PlayerData.PlayerFaceEnum.North;

            switch (action)
            {
                case GameMapAction.MoveAction.GoStraight:
                    PosX = 0;
                    PosY = -1;
                    playerFace = PlayerData.PlayerFaceEnum.South;
                    break;
                case GameMapAction.MoveAction.TurnLeft:
                    PosX = 1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.East;
                    break;
                case GameMapAction.MoveAction.TurnRight:
                    PosX = -1;
                    PosY = 0;
                    playerFace = PlayerData.PlayerFaceEnum.West;
                    break;
                case GameMapAction.MoveAction.GoBackward:
                    PosX = 0;
                    PosY = 1;
                    playerFace = PlayerData.PlayerFaceEnum.North;
                    break;
            }
        }
    }
}
