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
            if (ack != PlayerDataAck.Success)
            {
                Program.mainUI.ShowLogOnResult("接收角色資料失敗");
                return;
            }
            
            Program.mainUI.ShowLogOnResult($"ack={ack}");

            Program.mainUI.ShowPlayerInfo(playerData);
        }
    }
}
