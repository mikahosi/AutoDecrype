using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AutoDecryptCore;

namespace AutoDecrypeUnitTest
{
    [TestClass]
    public class UnitTestZipFilePasswordScanner
    {
        private string encryptedSampleFile = "EncryptSample.zip";
        private string plainSampleFile = "PlainSample.zip";
        private string samplePassword = "abc12345";

        [TestMethod]
        public void TestMethodIsEncrypted()
        {
            string[] passwordList = { };
            ZipFilePasswordScanner scanner = new ZipFilePasswordScanner(encryptedSampleFile, passwordList);
            Assert.AreEqual(true, scanner.isEncrypted());
        }

        [TestMethod]
        public void TestMethodIsNotEncrypted()
        {
            string[] passwordList = { };
            ZipFilePasswordScanner scanner = new ZipFilePasswordScanner(plainSampleFile, passwordList);
            Assert.AreEqual(false, scanner.isEncrypted());
        }

        [TestMethod]
        public void TestMethodPasswordFound()
        {
            List<string> passwordList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                passwordList.Add("pass-" + i.ToString("00000"));
            }
            passwordList.Add(samplePassword);

            ZipFilePasswordScanner scanner = new ZipFilePasswordScanner(encryptedSampleFile, passwordList.ToArray());
            Assert.AreEqual(samplePassword, scanner.SearchTruePassword());
        }

        [TestMethod]
        public void TestMethodPasswordNotFound()
        {
            List<string> passwordList = new List<string>();
            for (int i = 0;i < 100; i++)
            {
                passwordList.Add("pass-" + i.ToString("00000"));
            }

            ZipFilePasswordScanner scanner = new ZipFilePasswordScanner(encryptedSampleFile, passwordList.ToArray());
            Assert.AreEqual("", scanner.SearchTruePassword());
        }
    }
}
