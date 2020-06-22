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
            this.gbPlayerInfo = new System.Windows.Forms.GroupBox();
            this.tbPlayerUid = new System.Windows.Forms.TextBox();
            this.tbPlayerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPlayerHp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPlayerMp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPlayerAtk = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPlayerDef = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPlayerLevel = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPlayerExp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gbPlayerInfo.SuspendLayout();
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
            // gbPlayerInfo
            // 
            this.gbPlayerInfo.Controls.Add(this.label11);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerExp);
            this.gbPlayerInfo.Controls.Add(this.label10);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerLevel);
            this.gbPlayerInfo.Controls.Add(this.label9);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerDef);
            this.gbPlayerInfo.Controls.Add(this.label8);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerAtk);
            this.gbPlayerInfo.Controls.Add(this.label7);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerMp);
            this.gbPlayerInfo.Controls.Add(this.label6);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerHp);
            this.gbPlayerInfo.Controls.Add(this.label5);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerName);
            this.gbPlayerInfo.Controls.Add(this.tbPlayerUid);
            this.gbPlayerInfo.Controls.Add(this.label4);
            this.gbPlayerInfo.Location = new System.Drawing.Point(319, 40);
            this.gbPlayerInfo.Name = "gbPlayerInfo";
            this.gbPlayerInfo.Size = new System.Drawing.Size(200, 385);
            this.gbPlayerInfo.TabIndex = 8;
            this.gbPlayerInfo.TabStop = false;
            this.gbPlayerInfo.Text = "玩家資訊";
            // 
            // tbPlayerUid
            // 
            this.tbPlayerUid.Location = new System.Drawing.Point(79, 34);
            this.tbPlayerUid.Name = "tbPlayerUid";
            this.tbPlayerUid.ReadOnly = true;
            this.tbPlayerUid.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerUid.TabIndex = 8;
            // 
            // tbPlayerName
            // 
            this.tbPlayerName.Location = new System.Drawing.Point(79, 63);
            this.tbPlayerName.Name = "tbPlayerName";
            this.tbPlayerName.ReadOnly = true;
            this.tbPlayerName.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerName.TabIndex = 8;
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
            // tbPlayerHp
            // 
            this.tbPlayerHp.Location = new System.Drawing.Point(79, 92);
            this.tbPlayerHp.Name = "tbPlayerHp";
            this.tbPlayerHp.ReadOnly = true;
            this.tbPlayerHp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerHp.TabIndex = 8;
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
            // tbPlayerMp
            // 
            this.tbPlayerMp.Location = new System.Drawing.Point(79, 121);
            this.tbPlayerMp.Name = "tbPlayerMp";
            this.tbPlayerMp.ReadOnly = true;
            this.tbPlayerMp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerMp.TabIndex = 8;
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
            // tbPlayerAtk
            // 
            this.tbPlayerAtk.Location = new System.Drawing.Point(79, 150);
            this.tbPlayerAtk.Name = "tbPlayerAtk";
            this.tbPlayerAtk.ReadOnly = true;
            this.tbPlayerAtk.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerAtk.TabIndex = 8;
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
            // tbPlayerDef
            // 
            this.tbPlayerDef.Location = new System.Drawing.Point(79, 179);
            this.tbPlayerDef.Name = "tbPlayerDef";
            this.tbPlayerDef.ReadOnly = true;
            this.tbPlayerDef.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerDef.TabIndex = 8;
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
            // tbPlayerLevel
            // 
            this.tbPlayerLevel.Location = new System.Drawing.Point(79, 208);
            this.tbPlayerLevel.Name = "tbPlayerLevel";
            this.tbPlayerLevel.ReadOnly = true;
            this.tbPlayerLevel.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerLevel.TabIndex = 8;
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
            // tbPlayerExp
            // 
            this.tbPlayerExp.Location = new System.Drawing.Point(79, 237);
            this.tbPlayerExp.Name = "tbPlayerExp";
            this.tbPlayerExp.ReadOnly = true;
            this.tbPlayerExp.Size = new System.Drawing.Size(100, 23);
            this.tbPlayerExp.TabIndex = 8;
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
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbPlayerInfo);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbId);
            this.Name = "MainUI";
            this.Text = "UMD";
            this.gbPlayerInfo.ResumeLayout(false);
            this.gbPlayerInfo.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbPlayerInfo;
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
    }
}

