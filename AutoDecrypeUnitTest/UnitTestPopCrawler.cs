using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AutoDecryptCore;

namespace AutoDecrypeUnitTest
{
    [TestClass]
    public class UnitTestPopCrawler
    {
        private string serverAddress;
        private string userID;
        private string password;

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var reader = File.OpenText("popuser.password");
            serverAddress = reader.ReadLine();
            userID = reader.ReadLine();
            password = reader.ReadLine();
            reader.Close();
        }

        [TestMethod]
        public void TestMethodPasswordCrawlerForPop()
        {
            PasswordCrawlerForPop crawler = new PasswordCrawlerForPop(serverAddress, userID, password);
            crawler.Run();
        }
    }
}
