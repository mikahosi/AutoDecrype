using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using AutoDecryptCore;

namespace PasswordCrawler
{
    public partial class FormCrawler : Form
    {
        public FormCrawler()
        {
            InitializeComponent();
        }

        private void backgroundWorkerCrawler_DoWork(object sender, DoWorkEventArgs e)
        {
            PasswordCrawlerForPop craler = new PasswordCrawlerForPop(Properties.Settings.Default.Pop3Server, Properties.Settings.Default.Pop3UserID, Properties.Settings.Default.Pop3Password);
            craler.LastReceivedMessageID = Properties.Settings.Default.LastMessageID;
            craler.PasswordDataFileName = Properties.Settings.Default.PasswordDatabase;
            craler.Run();
            Properties.Settings.Default.LastMessageID = craler.LastReceivedMessageID;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// パスワード収集処理が動作していないなら起動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerKickCrawler_Tick(object sender, EventArgs e)
        {
            if (false == backgroundWorkerCrawler.IsBusy)
            {
                backgroundWorkerCrawler.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Pop3Server = textBoxServerAddress.Text;
            Properties.Settings.Default.Pop3UserID = textBoxUserID.Text;
            Properties.Settings.Default.Pop3Password = textBoxPassword.Text;
            Properties.Settings.Default.Save();
        }
    }
}
