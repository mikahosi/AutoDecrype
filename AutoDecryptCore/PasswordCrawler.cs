using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using OpenPop;

namespace AutoDecryptCore
{
    public class PasswordCrawler
    {
        public int minPasswordLength;
        public int maxPasswordLength;
        public bool hasNumberChar;
        public bool hasSmallAlphaChar;
        public bool hasLargeAlphaChar;
        public bool hasSymbolChar;

        public PasswordCrawler()
        {
            minPasswordLength = 4;
            maxPasswordLength = 16;
            hasNumberChar = false;
            hasSmallAlphaChar = false;
            hasLargeAlphaChar = false;
            hasSymbolChar = false;
        }

        /// <summary>
        /// パスワードの収集処理を開始する
        /// </summary>
        virtual public void Run()
        {

        }

        /// <summary>
        /// パスワード候補の条件を満たすトークンか判定する
        /// </summary>
        /// <param name="srceToken"></param>
        /// <returns></returns>
        public bool isCandidatePassword(string srceToken)
        {
            if (srceToken.Length < minPasswordLength)
                return false;

            if (srceToken.Length > maxPasswordLength)
                return false;

            if (hasNumberChar)
            {
                if (false == Regex.IsMatch(srceToken, "[0-9]"))
                {
                    return false;
                }
            }

            if (hasSmallAlphaChar)
            {
                if (false == Regex.IsMatch(srceToken, "[a-z]"))
                {
                    return false;
                }
            }

            if (hasLargeAlphaChar)
            {
                if (false == Regex.IsMatch(srceToken, "[A-Z]"))
                {
                    return false;
                }
            }

            if (hasSymbolChar)
            {
                if (false == Regex.IsMatch(srceToken, "[!\"#$%&'()=~|@`\\[{:\\*\\]};+,\\./\\\\\\<\\>?_]"))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 文書からパスワードの候補となる単語を抽出する
        /// </summary>
        /// <param name="srceText"></param>
        /// <returns></returns>
        public List<string> ParsePasswordTokens(string srceText)
        {
            List<string> resultTokens = new List<string>();

            var matchTokens = Regex.Matches(srceText, "[0-9a-zA-Z!\"#$%&'()=~|@`\\[{:\\*\\]};\\+,\\./\\\\\\<\\>?_]+");
            foreach (Match token in matchTokens)
            {
                resultTokens.Add(token.Value);

                if (token.Value.Substring(0,1) == ":")
                {
                    resultTokens.Add(token.Value.Substring(1));
                }
            }

            return resultTokens;
        }

        public void WritePassworList(List<DecryptPasswordArticle> passwords)
        {
            DecryptPasswordDatabase db = new DecryptPasswordDatabase();
            foreach(var rec in passwords)
            {
                db.AddDecryptPassword(rec.fromMailAddress, rec.decryptPassword, rec.mailSendDataTime);
            }
        }
    }
}
