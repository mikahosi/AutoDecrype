using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ionic.Zip;

namespace AutoDecryptCore
{
    public class ZipFilePasswordScanner
    {
        private string zipFileName;
        private List<string> passwordList;

        public ZipFilePasswordScanner(string sourceZipFileName, string[]sourcePasswordList)
        {
            zipFileName = sourceZipFileName;
            passwordList = new List<string>(sourcePasswordList);
        }

        /// <summary>
        /// 対象となるZIPファイルが暗号化されているか判定する
        /// </summary>
        /// <returns></returns>
        public bool isEncrypted()
        {
            if (ZipFile.CheckZipPassword(zipFileName, ""))
            {
                return false;
            }

            return true;
        }

        public string SearchTruePassword()
        {
            string truePassword = "";
            Parallel.ForEach(passwordList, (curtPassword, loopState) =>
            {
                try
                {
                    if (ZipFile.CheckZipPassword(zipFileName, curtPassword))
                    {
                        truePassword = curtPassword;
                        loopState.Break();
                    }
                }
                catch (BadCrcException exp)
                {

                }
            });

            return truePassword;
        }
    }
}
