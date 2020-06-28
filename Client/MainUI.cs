using Client.ClientSystem;
using Common;
using Common.Model.Command;
using Common.Model.GameMapComponents;
using Common.Model.PlayerDataComponents;
using Common.Model.UserComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainUI : Form
    {
        private delegate void ShowSystemLog(string text);
        private delegate void ShowPlayerData(PlayerData playerData);
        private delegate void ControlPlayerPanel(bool isVisible);
        private delegate void ControlPlayerAction(bool isVisible);
        private delegate void ControlPlayerLogin(bool isEnable);


        public MainUI()
        {
            InitializeComponent();
            OnControlPlayerPanel(false);
        }        

        private void StartRequest(Action action)
        {
            //非同步執行要求
            Task.Factory.StartNew(action);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbId.Text) || string.IsNullOrEmpty(tbPW.Text))
            {
                OnShowSystemLog($"Enter something, do not enter spaces or blanks.");
                return;
            }

            btnLogin.Enabled = false;

            StartRequest(ConnectAndLogin);
        }

        private void btnGoStraight_Click(object sender, EventArgs e)
        {
            GameMapClientSystem.Instance.OnMoveReq(GameMapAction.MoveAction.GoStraight);
            OnControlPlayerAction(false);
        }

        private void btnTurnLeft_Click(object sender, EventArgs e)
        {
            GameMapClientSystem.Instance.OnMoveReq(GameMapAction.MoveAction.TurnLeft);
            OnControlPlayerAction(false);
        }

        private void btnGoBackward_Click(object sender, EventArgs e)
        {
            GameMapClientSystem.Instance.OnMoveReq(GameMapAction.MoveAction.GoBackward);
            OnControlPlayerAction(false);
        }

        private void btnTurnRight_Click(object sender, EventArgs e)
        {
            GameMapClientSystem.Instance.OnMoveReq(GameMapAction.MoveAction.TurnRight);
            OnControlPlayerAction(false);
        }

        private void ConnectAndLogin()
        {
            var userInfo = new User
            {
                PlayerUid = 0,
                UserId = tbId.Text,
                UserPwd = tbPW.Text,
            };
            SocketClientManager.Instance.StartClientAndLogin(userInfo);
        }

        public void OnControlPlayerPanel(bool isVisible)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ControlPlayerPanel(OnControlPlayerPanel), new object[] { isVisible }); ;
            }
            else
            {
                gbPlayerData.Visible = isVisible;
                gbContolPlayer.Visible = isVisible;
            }
        }

        public void OnControlPlayerAction(bool isVisible)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ControlPlayerAction(OnControlPlayerAction), new object[] { isVisible }); ;
            }
            else
            {
                gbContolPlayer.Visible = isVisible;
            }
        }

        public void OnShowSystemLog(string str)
        {
            if (InvokeRequired)
            {
                tbResult.BeginInvoke(new ShowSystemLog(OnShowSystemLog), new object[] { str });
            }
            else
            {
                tbResult.AppendText(str + Environment.NewLine);
            }
        }

        public void ControlLoginBtn(bool isEnbale)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ControlPlayerLogin(ControlLoginBtn), new object[] { isEnbale }); ;
            }
            else
            {
                btnLogin.Enabled = isEnbale;
            }
        }

        public void OnShowPlayerData(PlayerData playerData)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowPlayerData(OnShowPlayerData), new object[] { playerData });
            }
            else
            {
                tbPlayerUid.Text = $"{playerData.PlayerUid}";
                tbPlayerName.Text = $"{playerData.Name}";
                tbPlayerHp.Text = $"{playerData.HP}";
                tbPlayerMp.Text = $"{playerData.MP}";
                tbPlayerAtk.Text = $"{playerData.Atk}";
                tbPlayerDef.Text = $"{playerData.Def}";
                tbPlayerLevel.Text = $"{playerData.Level}";
                tbPlayerExp.Text = $"{playerData.Exp}";
                tbMapSeed.Text = $"{playerData.Exp}";
                tbMapPos.Text = $"[{playerData.PosX},{playerData.PosY}]";
                tbPlayerFace.Text = $"{Enum.GetName(typeof(PlayerData.PlayerFaceEnum), playerData.PlayeyFace)}";
            }
        }
    }
}
