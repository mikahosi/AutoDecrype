using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDecryptCore
{
    public class PasswordCrawlerForPop : PasswordCrawler
    {
        protected string serverAddress;
        protected string userID;
        protected string password;


        public PasswordCrawlerForPop(string srceServerAddress, string srceUserID, string srcePassword)
        {
            serverAddress = srceServerAddress;
            srceUserID = userID;
            srcePassword = password;
        }

        /// <summary>
        /// パスワードの収集処理を開始する
        /// </summary>
        override public void Run()
        {
        }
    }
}
