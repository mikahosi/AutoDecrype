namespace PasswordCrawler
{
    partial class FormCrawler
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backgroundWorkerCrawler = new System.ComponentModel.BackgroundWorker();
            this.timerKickCrawler = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserID = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonReSync = new System.Windows.Forms.Button();
            this.statusStripBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.statusStripBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerCrawler
            // 
            this.backgroundWorkerCrawler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCrawler_DoWork);
            this.backgroundWorkerCrawler.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerCrawler_ProgressChanged);
            this.backgroundWorkerCrawler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerCrawler_RunWorkerCompleted);
            // 
            // timerKickCrawler
            // 
            this.timerKickCrawler.Enabled = true;
            this.timerKickCrawler.Interval = 60000;
            this.timerKickCrawler.Tick += new System.EventHandler(this.timerKickCrawler_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "POP3 Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "User ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // textBoxUserID
            // 
            this.textBoxUserID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PasswordCrawler.Properties.Settings.Default, "Pop3UserID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUserID.Location = new System.Drawing.Point(99, 41);
            this.textBoxUserID.Name = "textBoxUserID";
            this.textBoxUserID.Size = new System.Drawing.Size(273, 19);
            this.textBoxUserID.TabIndex = 3;
            this.textBoxUserID.Text = global::PasswordCrawler.Properties.Settings.Default.Pop3UserID;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PasswordCrawler.Properties.Settings.Default, "Pop3Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPassword.Location = new System.Drawing.Point(99, 66);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(273, 19);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.Text = global::PasswordCrawler.Properties.Settings.Default.Pop3Password;
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PasswordCrawler.Properties.Settings.Default, "Pop3Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxServerAddress.Location = new System.Drawing.Point(99, 16);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(273, 19);
            this.textBoxServerAddress.TabIndex = 5;
            this.textBoxServerAddress.Text = global::PasswordCrawler.Properties.Settings.Default.Pop3Server;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(297, 94);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "設定";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonReSync
            // 
            this.buttonReSync.Location = new System.Drawing.Point(14, 94);
            this.buttonReSync.Name = "buttonReSync";
            this.buttonReSync.Size = new System.Drawing.Size(75, 23);
            this.buttonReSync.TabIndex = 7;
            this.buttonReSync.Text = "再同期";
            this.buttonReSync.UseVisualStyleBackColor = true;
            this.buttonReSync.Click += new System.EventHandler(this.buttonReSync_Click);
            // 
            // statusStripBar
            // 
            this.statusStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusMessage});
            this.statusStripBar.Location = new System.Drawing.Point(0, 128);
            this.statusStripBar.Name = "statusStripBar";
            this.statusStripBar.Size = new System.Drawing.Size(384, 22);
            this.statusStripBar.TabIndex = 8;
            this.statusStripBar.Text = "statusStrip1";
            // 
            // toolStripStatusMessage
            // 
            this.toolStripStatusMessage.Name = "toolStripStatusMessage";
            this.toolStripStatusMessage.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusMessage.Text = "toolStripStatusMessage";
            // 
            // timerStatusUpdate
            // 
            this.timerStatusUpdate.Enabled = true;
            this.timerStatusUpdate.Interval = 500;
            this.timerStatusUpdate.Tick += new System.EventHandler(this.timerStatusUpdate_Tick);
            // 
            // FormCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 150);
            this.Controls.Add(this.statusStripBar);
            this.Controls.Add(this.buttonReSync);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.textBoxServerAddress);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormCrawler";
            this.Text = "Password Crawler";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.statusStripBar.ResumeLayout(false);
            this.statusStripBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerCrawler;
        private System.Windows.Forms.Timer timerKickCrawler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUserID;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonReSync;
        private System.Windows.Forms.StatusStrip statusStripBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMessage;
        private System.Windows.Forms.Timer timerStatusUpdate;
    }
}

