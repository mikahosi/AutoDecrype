using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AutoDecryptCore;

namespace AutoDecrypeUnitTest
{
    /// <summary>
    /// UnitTestCreateOrReplaceBlankDatabase の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTestDecryptPasswordDatabase
    {
        public UnitTestDecryptPasswordDatabase()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

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
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        [TestMethod]
        public void CreateOrReplaceBlankDatabase()
        {
            DecryptPasswordDatabase db = new DecryptPasswordDatabase();

            db.RemoveDatabase();

            Assert.AreEqual(false, db.ExistDatabase());

            db.CreateOrReplaceBlankDatabase();

            Assert.AreEqual(true, db.ExistDatabase());

            db.CreateOrReplaceBlankDatabase();

            Assert.AreEqual(true, db.ExistDatabase());
        }

        [TestMethod]
        public void AddOrGetDecryptPassword()
        {
            string testAddress = "mailaddress";
            string testPassword = "decryptPassword";
            DateTime dateDate = DateTime.Now;

            DecryptPasswordDatabase db = new DecryptPasswordDatabase();
            db.CreateOrReplaceBlankDatabase();
            db.AddDecryptPassword(testAddress, testPassword, dateDate);

            var recs = db.GetDecryptPassword();
            Assert.AreEqual(1, recs.Count);
            Assert.AreEqual(testAddress, recs[0].fromMailAddress);
            Assert.AreEqual(testPassword, recs[0].decryptPassword);
            Assert.AreEqual(dateDate.ToLongDateString(), recs[0].mailSendDataTime.ToLongDateString());
            Assert.AreEqual(dateDate.ToLongTimeString(), recs[0].mailSendDataTime.ToLongTimeString());
        }

        [TestMethod]
        public void AddOrGetDecryptPasswordMulti()
        {
            string testAddress = "mailaddress";
            string testPassword = "decryptPassword";
            DateTime dateDate = DateTime.Now;

            DecryptPasswordDatabase db = new DecryptPasswordDatabase();
            db.CreateOrReplaceBlankDatabase();

            for (int i = 0; i < 100; i++)
            {
                db.AddDecryptPassword(testAddress + i.ToString("0000"),
                                        testPassword + i.ToString("0000"), 
                                        dateDate);
            }

            var recs = db.GetDecryptPassword();
            Assert.AreEqual(100, recs.Count);

            int recCnt = 0;
            foreach (var r in recs)
            {
                Assert.AreEqual(testAddress + recCnt.ToString("0000"), r.fromMailAddress);
                Assert.AreEqual(testPassword + recCnt.ToString("0000"), r.decryptPassword);
                Assert.AreEqual(dateDate.ToLongDateString(), r.mailSendDataTime.ToLongDateString());
                Assert.AreEqual(dateDate.ToLongTimeString(), r.mailSendDataTime.ToLongTimeString());
                recCnt++;
            }
        }


        [TestMethod]
        public void GetDecryptPasswordList()
        {
            string testAddress = "mailaddress";
            string testPassword = "decryptPassword";
            DateTime dateDate = DateTime.Now;

            DecryptPasswordDatabase db = new DecryptPasswordDatabase();
            db.CreateOrReplaceBlankDatabase();

            for (int i = 0; i < 100; i++)
            {
                db.AddDecryptPassword(testAddress + i.ToString("0000"),
                                        testPassword + i.ToString("0000"),
                                        dateDate);
            }

            var recs = db.GetPasswordList();
            Assert.AreEqual(100, recs.Count);

            int recCnt = 0;
            foreach (var r in recs)
            {
                Assert.AreEqual(testPassword + recCnt.ToString("0000"), r);
                recCnt++;
            }
        }
    }
}
