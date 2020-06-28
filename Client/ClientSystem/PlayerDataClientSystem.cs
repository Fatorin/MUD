using Client.Base;
using Common.Model.PlayerDataComponents;
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
                Program.mainUI.OnShowSystemLog("接收角色資料失敗");
                return;
            }
            
            Program.mainUI.OnShowSystemLog($"ack={ack}");
            Program.mainUI.OnControlPlayerPanel(true);
            Program.PlayerDataInfo = playerData;
            Program.mainUI.OnShowPlayerData(Program.PlayerDataInfo);
        }
    }
}
