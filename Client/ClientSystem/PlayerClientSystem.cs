using Client.Base;
using Common.Model.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ClientSystem
{
    public class PlayerClientSystem : BaseClientSystem
    {
        public static PlayerClientSystem Instance { get; } = new PlayerClientSystem();

        public PlayerClientSystem()
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
