using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using OpenPop;
using OpenPop.Common;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace AutoDecryptCore
{
    public class PasswordCrawlerForPop : PasswordCrawler
    {
        protected string serverAddress;
        protected string userID;
        protected string password;
        protected string lastReceivedMessageID;

        public string LastReceivedMessageID
        {
            get
            {
                return lastReceivedMessageID;
            }
            set
            {
                lastReceivedMessageID = value;
            }
        }

        public PasswordCrawlerForPop(string srceServerAddress, string srceUserID, string srcePassword)
        {
            serverAddress = srceServerAddress;
            userID = srceUserID;
            password = srcePassword;
        }

        private List<Message> DownloadMessages()
        {
            List<Message> allMessages = new List<Message>();
            List<string> uids = new List<string>();

            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(serverAddress, 110, false);

                // Authenticate ourselves towards the server
                client.Authenticate(userID, password);

                // download uids
                uids = client.GetMessageUids();

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    if (LastReceivedMessageID == uids[i - 1])
                        break;

                    Console.WriteLine("Download Message, {0} / {1}", i, messageCount);
                    Message curtMessage = client.GetMessage(i);
                    allMessages.Add(curtMessage);
                    LastReceivedMessageID = uids[i - 1];
                }
                client.Disconnect();
            }

            return allMessages;
        }

        private List<DecryptPasswordArticle> PursePasswordTokens(List<Message> allMessages)
        {
            List<DecryptPasswordArticle> parseText = new List<DecryptPasswordArticle>();
            foreach (var message in allMessages)
            {
                if (null == message.FindFirstPlainTextVersion())
                    continue;

                var bodyEncodeing = message.FindFirstPlainTextVersion().BodyEncoding;
                Debug.Write("EncodingName = "); Debug.WriteLine(bodyEncodeing.EncodingName);

                string bodyText = bodyEncodeing.GetString(message.FindFirstPlainTextVersion().Body);
                Debug.WriteLine("bodyText = "); Debug.WriteLine(bodyText);

                foreach (string token in ParsePasswordTokens(bodyText))
                {
                    if (isCandidatePassword(token))
                    {
                        DecryptPasswordArticle newRec = new DecryptPasswordArticle();
                        newRec.fromMailAddress = message.Headers.From.Address;
                        newRec.mailSendDataTime = message.Headers.DateSent;
                        newRec.decryptPassword = token;
                        parseText.Add(newRec);
                    }
                }
            }

            return parseText;
        }

        /// <summary>
        /// パスワードの収集処理を開始する
        /// </summary>
        override public void Run()
        {
            List<Message> allMessages = DownloadMessages();

            List<DecryptPasswordArticle> parseText = PursePasswordTokens(allMessages);

            WritePassworList(parseText);
        }
    }
}
