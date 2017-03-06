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
            Console.WriteLine("Load Passwords Count = {0}", passwordList.Count);

            ZipFilePasswordScanner passwordScan = new ZipFilePasswordScanner(args[0], passwordList.ToArray());
            if (passwordScan.isEncrypted())
            {
                string truePassword = passwordScan.SearchTruePassword();
                Console.WriteLine("Detect Password = {0}", truePassword);

                if (truePassword != "")
                {
                    string exportPath = passwordScan.Decode(truePassword);
                    Console.WriteLine("Exported Encrypt File = {0}", exportPath);

                    System.Diagnostics.Process.Start(exportPath);
                }
            }
            else
            {
                string exportPath = passwordScan.Decode("");
                System.Diagnostics.Process.Start(exportPath);
            }
        }
    }
}
