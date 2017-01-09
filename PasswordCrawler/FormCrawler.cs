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

        }

        private void timerKickCrawler_Tick(object sender, EventArgs e)
        {

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Pop3Server = textBoxServerAddress.Text;
            Properties.Settings.Default.Pop3UserID = textBoxUserID.Text;
            Properties.Settings.Default.Pop3Password = textBoxPassword.Text;
            Properties.Settings.Default.Save();
        }
    }
}
