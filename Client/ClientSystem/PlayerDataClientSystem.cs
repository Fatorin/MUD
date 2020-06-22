using Client.Base;
using Common.Model.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class PlayerDataClientSystem : BaseClientSystem
    {
        public static PlayerDataClientSystem Instance { get; } = new PlayerDataClientSystem();

        public PlayerDataClientSystem()
        {
            mappings.TryAdd((int)PlayerDataCommand.PlayerDataResp, OnPlayerDataResp);
        }

        public void OnPlayerDataResp(byte[] data)
        {
            PlayerDataRespPayload.ParsePayload(data, out PlayerDataAck ack, out var playerData);
            Program.mainUI.ShowLogOnResult($"ack={ack}");
            Program.mainUI.ShowLogOnResult($"playerData={playerData}");
        }
    }
}
