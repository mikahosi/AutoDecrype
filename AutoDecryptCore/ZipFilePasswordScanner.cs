using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

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
                catch (Exception exp)
                {

                }
            });

            return truePassword;
        }

        public string Decode(string password)
        {
            string exportPath = Environment.ExpandEnvironmentVariables("%TEMP%") + "\\AutoDecrypt\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
            Directory.CreateDirectory(exportPath);
            ReadOptions readOptions = new ReadOptions();
            readOptions.Encoding = System.Text.Encoding.GetEncoding("shift_jis");
            ZipFile zipFile = ZipFile.Read(zipFileName, readOptions);
            zipFile.Password = password;
            foreach (string entryName in zipFile.EntryFileNames)
            {
                Console.WriteLine(entryName);
            }

            zipFile.ExtractAll(exportPath, ExtractExistingFileAction.OverwriteSilently);
            return exportPath;
        }

        public void Decode(string exportDir, string password)
        {
        }

    }
}
