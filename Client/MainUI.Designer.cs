namespace Client
{
    partial class MainUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.gbPlayerData = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbMapPos = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbMapSeed = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbPlayerExp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPlayerLevel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPlayerDef = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPlayerAtk = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPlayerMp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPlayerHp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPlayerName = new System.Windows.Forms.TextBox();
            this.tbPlayerUid = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gbContolPlayer = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.gbPlayerData.SuspendLayout();
            this.gbContolPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(12, 40);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(100, 23);
            this.tbId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "PW";
            // 
            // tbPW
            // 
            this.tbPW.Location = new System.Drawing.Point(131, 40);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(100, 23);
            this.tbPW.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Result";
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 95);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(301, 330);
            this.tbResult.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(238, 40);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "玩家編號：";
            // 
            // gbPlayerData
            // 
            this.gbPlayerData.Controls.Add(this.label13);
            this.gbPlayerData.Controls.Add(this.tbMapPos);
            this.gbPlayerData.Controls.Add(this.label12);
            this.gbPlayerData.Controls.Add(this.tbMapSeed);
            this.gbPlayerData.Controls.Add(this.label11);
            this.gbPlayerData.Controls.Add(this.tbPlayerExp);
            this.gbPlayerData.Controls.Add(this.label10);
            this.gbPlayerData.Controls.Add(this.tbPlayerLevel);
            this.gbPlayerData.Controls.Add(this.label9);
            this.gbPlayerData.Controls.Add(this.tbPlayerDef);
            this.gbPlayerData.Controls.Add(this.label8);
            this.gbPlayerData.Controls.Add(this.tbPlayerAtk);
            this.gbPlayerData.Controls.Add(this.label7);
            this.gbPlayerData.Controls.Add(this.tbPlayerMp);
            this.gbPlayerData.Controls.Add(this.label6);
            this.gbPlayerData.Controls.Add(this.tbPlayerHp);
            this.gbPlayerData.Controls.Add(this.label5);
            this.gbPlayerData.Controls.Add(this.tbPlayerName);
            this.gbPlayerData.Controls.Add(this.tbPlayerUid);
            this.gbPlayerData.Controls.Add(this.label4);
            this.gbPlayerData.Location = new System.Drawing.Point(319, 40);
            this.gbPlayerData.Name = "gbPlayerData";
            this.gbPlayerData.Size = new System.Drawing.Size(200, 385);
            this.gbPlayerData.TabIndex = 8;
            this.gbPlayerData.TabStop = false;
            this.gbPlayerData.Text = "玩家資訊";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 298);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 15);
            this.label13.TabIndex = 7;
            this.label13.Text = "座標：";
            // 
            // tbMapPos
            // 
            this.tbMapPos.Location = new System.Drawing.Point(79, 295);
            this.tbMapPos.Name = "tbMapPos";
            this.tbMapPos.ReadOnly = true;
            this.tbMapPos.Size = new System.Drawing.Size(100, 23);
            this.tbMapPos.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 269);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "地圖名稱：";
            // 
            // tbMapSeed
            // 
            this.tbMapSeed.Location = new System.Drawing.Point(79, 266);
            this.tbMapSeed.Name = "tbMapSeed";
            this.tbMapSeed.ReadOnly = true;
            this.tbMapSeed.Size = new System.Drawing.Size(100, 23);
            this.tbMapSeed.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "經驗值：";
            // 
            // tbPlayerExp
            // 
            this.tbPlayerExp.Location = new System.Drawing.Point(79, 237);
            this.tbPlayerExp.Name = "tbPlayerExp";
            this.tbPlayerExp.ReadOnly = true;
            this.tbPlayerExp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerExp.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 15);
            this.label10.TabIndex = 7;
            this.label10.Text = "等級：";
            // 
            // tbPlayerLevel
            // 
            this.tbPlayerLevel.Location = new System.Drawing.Point(79, 208);
            this.tbPlayerLevel.Name = "tbPlayerLevel";
            this.tbPlayerLevel.ReadOnly = true;
            this.tbPlayerLevel.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerLevel.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "防禦力：";
            // 
            // tbPlayerDef
            // 
            this.tbPlayerDef.Location = new System.Drawing.Point(79, 179);
            this.tbPlayerDef.Name = "tbPlayerDef";
            this.tbPlayerDef.ReadOnly = true;
            this.tbPlayerDef.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerDef.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "攻擊力：";
            // 
            // tbPlayerAtk
            // 
            this.tbPlayerAtk.Location = new System.Drawing.Point(79, 150);
            this.tbPlayerAtk.Name = "tbPlayerAtk";
            this.tbPlayerAtk.ReadOnly = true;
            this.tbPlayerAtk.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerAtk.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "魔力值：";
            // 
            // tbPlayerMp
            // 
            this.tbPlayerMp.Location = new System.Drawing.Point(79, 121);
            this.tbPlayerMp.Name = "tbPlayerMp";
            this.tbPlayerMp.ReadOnly = true;
            this.tbPlayerMp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerMp.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "生命值：";
            // 
            // tbPlayerHp
            // 
            this.tbPlayerHp.Location = new System.Drawing.Point(79, 92);
            this.tbPlayerHp.Name = "tbPlayerHp";
            this.tbPlayerHp.ReadOnly = true;
            this.tbPlayerHp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerHp.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "玩家名稱：";
            // 
            // tbPlayerName
            // 
            this.tbPlayerName.Location = new System.Drawing.Point(79, 63);
            this.tbPlayerName.Name = "tbPlayerName";
            this.tbPlayerName.ReadOnly = true;
            this.tbPlayerName.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerName.TabIndex = 8;
            // 
            // tbPlayerUid
            // 
            this.tbPlayerUid.Location = new System.Drawing.Point(79, 34);
            this.tbPlayerUid.Name = "tbPlayerUid";
            this.tbPlayerUid.ReadOnly = true;
            this.tbPlayerUid.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerUid.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "向左走";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "向前走";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // gbContolPlayer
            // 
            this.gbContolPlayer.Controls.Add(this.button4);
            this.gbContolPlayer.Controls.Add(this.button3);
            this.gbContolPlayer.Controls.Add(this.button1);
            this.gbContolPlayer.Controls.Add(this.button2);
            this.gbContolPlayer.Location = new System.Drawing.Point(533, 345);
            this.gbContolPlayer.Name = "gbContolPlayer";
            this.gbContolPlayer.Size = new System.Drawing.Size(255, 80);
            this.gbContolPlayer.TabIndex = 9;
            this.gbContolPlayer.TabStop = false;
            this.gbContolPlayer.Text = "人物行動控制";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(170, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "向右走";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(90, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "向後走";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbContolPlayer);
            this.Controls.Add(this.gbPlayerData);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbId);
            this.Name = "MainUI";
            this.Text = "UMD";
            this.gbPlayerData.ResumeLayout(false);
            this.gbPlayerData.PerformLayout();
            this.gbContolPlayer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbPlayerData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPlayerName;
        private System.Windows.Forms.TextBox tbPlayerUid;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbPlayerExp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPlayerLevel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPlayerDef;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPlayerAtk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPlayerMp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPlayerHp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbMapPos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbMapSeed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gbContolPlayer;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

