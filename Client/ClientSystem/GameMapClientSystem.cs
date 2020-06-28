using Client.Base;
using Common.Model.GameMapComponents;

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

        public void OnMoveReq(GameMapAction.MoveAction moveAction)
        {
            //封包只傳往前後左右的動作
            var payload = GameMapMoveReqPayload.CreatePayload(moveAction);

            SocketClientManager.Instance.Send(payload);
        }

        private void OnMoveResp(byte[] data)
        {
            //封包接收移動的位置與面向的方向
            GameMapMoveRespPayload.ParsePayload(data, out var ackCode, out var posX, out var posY, out var playerFace);

            if (ackCode != GameMapAck.Success)
            {
                Program.mainUI.OnShowSystemLog($"移動錯誤，錯誤代碼：{ackCode}");
                return;
            }

            Program.PlayerDataInfo.PosX = posX;
            Program.PlayerDataInfo.PosY = posY;
            Program.PlayerDataInfo.PlayeyFace = playerFace;
            Program.mainUI.OnShowPlayerData(Program.PlayerDataInfo);
            Program.mainUI.OnControlPlayerAction(true);
        }

        private void OnEventResp(byte[] data)
        {

        }
    }
}
