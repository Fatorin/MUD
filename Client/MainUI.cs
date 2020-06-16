using Common;
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
        private BackgroundWorker bgWork;

        public MainUI()
        {
            InitializeComponent();
            InitBackgroundWorker();
        }

        private void InitBackgroundWorker()
        {
            //因為 Net Core Form不支援只好自己手寫一個
            bgWork = new BackgroundWorker();
            bgWork.WorkerReportsProgress = true;
            bgWork.WorkerSupportsCancellation = true;
            bgWork.DoWork += new DoWorkEventHandler(bgWork_DoWork);
            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWork_RunWorkerCompleted);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbId.Text) || string.IsNullOrEmpty(tbPW.Text))
            {
                ShowLogOnResult($"Enter something, do not enter spaces or blanks.");
                return;
            }

            bgWorkerGoFunc(ConnectAndLogin);
        }

        private void bgWorkerGoFunc(Action func)
        {
            if (!bgWork.IsBusy)
            {
                bgWork.RunWorkerAsync(func);
            }
        }

        private void bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            /*if (!CommandReqDict.TryGetValue((int)e.Argument, out var function))
            {
                ShowLogOnResult("Not found mapping req function.");
                return;
            }
            function();*/
            ShowLogOnResult("Func Start.");
            ConnectAndLogin();
        }
        private void bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLogOnResult("Func Finish.");
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
