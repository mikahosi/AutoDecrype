using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDecryptCore
{
    class VerifyZipFile
    {
        private string zipFileName;
        private List<string> passwordList;

        public VerifyZipFile(string sourceZipFileName, string[]sourcePasswordList)
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
            return false;
        }

        public string searchTruePassword()
        {
            return "";
        }
    }
}
