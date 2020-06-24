using Common;
using Common.Model.Command;
using Common.Model.PlayerData;
using Common.Model.User;
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
        private delegate void UpdateShowLog(string text);
        private delegate void UpdatePlayerData(PlayerData playerData);
        private delegate void ControlPlayerDataAndControl(bool isVisible);
        private delegate void ControlPlayerLogin(bool isEnable);

        public MainUI()
        {
            InitializeComponent();
            InitDisableInfoAndControl(false);
        }

        public void InitDisableInfoAndControl(bool isVisible)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ControlPlayerDataAndControl(InitDisableInfoAndControl), new object[] { isVisible }); ;
            }
            else
            {
                gbPlayerData.Visible = isVisible;
                gbContolPlayer.Visible = isVisible;
            }
        }

        private void StartRequest(Action action)
        {
            Task.Factory.StartNew(action);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbId.Text) || string.IsNullOrEmpty(tbPW.Text))
            {
                ShowLogOnResult($"Enter something, do not enter spaces or blanks.");
                return;
            }

            btnLogin.Enabled = false;

            StartRequest(ConnectAndLogin);
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

        public void ShowLogOnResult(string str)
        {
            if (InvokeRequired)
            {
                tbResult.BeginInvoke(new UpdateShowLog(ShowLogOnResult), new object[] { str });
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

        public void ShowPlayerInfo(PlayerData playerData)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdatePlayerData(ShowPlayerInfo), new object[] { playerData });
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
            }
        }
    }

    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
