using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AutoDecryptCore;

namespace AutoDecryptConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (0 == args.Count())
                return;

            Console.WriteLine("Target File = {0}", args[0]);
            if (false == File.Exists(args[0]))
                return;

            string pathname = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.PasswordDatabase);
            DecryptPasswordDatabase passwordData = new DecryptPasswordDatabase(pathname);
            List<string> passwordList = passwordData.GetPasswordList();

            ZipFilePasswordScanner passwordScan = new ZipFilePasswordScanner(args[0], passwordList.ToArray());
            if (passwordScan.isEncrypted())
            {
                string truePassword = passwordScan.SearchTruePassword();
                string exportPath = passwordScan.Decode(truePassword);
                System.Diagnostics.Process.Start(exportPath);

            }
        }
    }
}
