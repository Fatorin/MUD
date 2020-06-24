using Client.Base;
using Common.Model.GameMap;
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
            mappings.TryAdd((int)GameMapCommand.MoveResp, OnMoveResp);
            mappings.TryAdd((int)GameMapCommand.EventResp, OnEventResp);
        }


        private void OnMoveResp(byte[] data)
        {
            GameMapMoveRespPayload.ParsePayload(data, out var ackCode, out var posX, out var posY);
            


        }

        private void OnEventResp(byte[] data)
        {

        }
    }
}
