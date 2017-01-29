using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AutoDecryptCore;

namespace AutoDecrypeUnitTest
{
    /// <summary>
    /// UnitTestPasswordCrawler の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTestPasswordCrawler
    {
        public UnitTestPasswordCrawler()
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

        [TestMethod]
        public void TestMethodCandidatePasswordLength()
        {
            PasswordCrawler crawler = new PasswordCrawler();

            crawler.minPasswordLength = 4;
            crawler.maxPasswordLength = 8;
            Assert.AreEqual(true, crawler.isCandidatePassword("0000"));
            Assert.AreEqual(false, crawler.isCandidatePassword("000"));

            Assert.AreEqual(true, crawler.isCandidatePassword("00000000"));
            Assert.AreEqual(false, crawler.isCandidatePassword("000000000"));
        }

        [TestMethod]
        public void TestMethodCandidatePasswordHasChars()
        {
            PasswordCrawler crawler = new PasswordCrawler();

            crawler.minPasswordLength = 4;
            crawler.maxPasswordLength = 8;

            crawler.hasNumberChar = false;
            crawler.hasSmallAlphaChar = false;
            crawler.hasLargeAlphaChar = false;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(true, crawler.isCandidatePassword("0000"));
            Assert.AreEqual(true, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(true, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(true, crawler.isCandidatePassword("/*-+"));

            crawler.hasNumberChar = true;
            crawler.hasSmallAlphaChar = false;
            crawler.hasLargeAlphaChar = false;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(true, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));

            crawler.hasNumberChar = false;
            crawler.hasSmallAlphaChar = true;
            crawler.hasLargeAlphaChar = false;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(true, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));

            crawler.hasNumberChar = false;
            crawler.hasSmallAlphaChar = false;
            crawler.hasLargeAlphaChar = true;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(true, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));

            crawler.hasNumberChar = false;
            crawler.hasSmallAlphaChar = false;
            crawler.hasLargeAlphaChar = false;
            crawler.hasSymbolChar = true;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(true, crawler.isCandidatePassword("/*-+"));

            crawler.hasNumberChar = true;
            crawler.hasSmallAlphaChar = true;
            crawler.hasLargeAlphaChar = false;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));
            Assert.AreEqual(true, crawler.isCandidatePassword("01ab"));

            crawler.hasNumberChar = true;
            crawler.hasSmallAlphaChar = true;
            crawler.hasLargeAlphaChar = true;
            crawler.hasSymbolChar = false;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));
            Assert.AreEqual(true, crawler.isCandidatePassword("01aB"));

            crawler.hasNumberChar = true;
            crawler.hasSmallAlphaChar = true;
            crawler.hasLargeAlphaChar = true;
            crawler.hasSymbolChar = true;
            Assert.AreEqual(false, crawler.isCandidatePassword("0123"));
            Assert.AreEqual(false, crawler.isCandidatePassword("abcd"));
            Assert.AreEqual(false, crawler.isCandidatePassword("WXYZ"));
            Assert.AreEqual(false, crawler.isCandidatePassword("/*-+"));
            Assert.AreEqual(true, crawler.isCandidatePassword("0aB+"));
        }

        [TestMethod]
        public void TestMethodParsePasswordTokens()
        {
            PasswordCrawler crawler = new PasswordCrawler();

            List<string> passwordTokens;
            passwordTokens = crawler.ParsePasswordTokens("パスワード候補を含まない文字列");
            Assert.AreEqual(0, passwordTokens.Count);

            passwordTokens = crawler.ParsePasswordTokens("パスワードとしてabcd0123を含む場合");
            Assert.AreEqual(1, passwordTokens.Count);
            Assert.AreEqual("abcd0123", passwordTokens[0]);

            passwordTokens = crawler.ParsePasswordTokens("パスワードとしてabcd0123と-+*/::を含む場合");
            Assert.AreEqual(2, passwordTokens.Count);
            Assert.AreEqual("abcd0123", passwordTokens[0]);
            Assert.AreEqual("-+*/::", passwordTokens[1]);

            passwordTokens = crawler.ParsePasswordTokens("パスワードとしてabcd0123と:+-*/を含む場合");
            Assert.AreEqual(3, passwordTokens.Count);
            Assert.AreEqual("abcd0123", passwordTokens[0]);
            Assert.AreEqual(":+-*/", passwordTokens[1]);
            Assert.AreEqual("+-*/", passwordTokens[2]);

            passwordTokens = crawler.ParsePasswordTokens("全角は０１２３、ａａａａ、ＡＡＡＡ、＋－＊／パスワードとして認識しない");
            Assert.AreEqual(0, passwordTokens.Count);
        }
    }
}
