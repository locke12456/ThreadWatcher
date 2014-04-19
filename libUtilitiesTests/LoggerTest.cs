using libUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace libUtilitiesTests
{


    /// <summary>
    ///這是 LoggerTest 的測試類別，應該包含
    ///所有 LoggerTest 單元測試
    ///</summary>
    [TestClass()]
    public class LoggerTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合的相關資訊與功能
        ///的測試內容。
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

        #region 其他測試屬性
        // 
        //您可以在撰寫測試時，使用下列的其他屬性:
        //
        //在執行類別中的第一項測試之前，先使用 ClassInitialize 執行程式碼
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //在執行類別中的所有測試之後，使用 ClassCleanup 執行程式碼
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //在執行每一項測試之前，先使用 TestInitialize 執行程式碼
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //在執行每一項測試之後，使用 TestCleanup 執行程式碼
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        
        /// <summary>
        ///getInstance 的測試
        ///</summary>
        [TestMethod()]
        public void getInstanceTest()
        {
            Logger actual = null ;
            actual = Logger.getInstance();
            Assert.IsTrue(actual != null);
            Logger.Distroy();
        }
        /// <summary>
        ///System log 的測試
        ///</summary>
        [TestMethod()]
        public void SystemLogTest()
        {
            Logger target = Logger.getInstance();
            string loginfo = _writeLog(LogTypes.SYSTEM);
            FileInfo file = new FileInfo(loginfo);
            DateTime dt = file.LastWriteTime;
            string msg = "test case SystemLogTest";
            target.System(msg);
            Logger.Distroy();
            file = new FileInfo(loginfo);
            Assert.IsTrue(dt < file.LastWriteTime);
        }

        /// <summary>
        ///Debug 的測試
        ///</summary>
        [TestMethod()]
        public void DebugTest()
        {
#if DEBUG
            Logger target = Logger.getInstance();
            string loginfo = _writeLog(LogTypes.DEBUG);
            FileInfo file = new FileInfo(loginfo);
            DateTime dt = file.LastWriteTime;
            string msg = "test case DebugTest";
            target.Debug(msg);
            Logger.Distroy();
            file = new FileInfo(loginfo);
            Assert.IsTrue(dt < file.LastWriteTime);
#endif
        }

        /// <summary>
        /// log 的測試
        ///</summary>
        [TestMethod()]
        public void LogTest()
        {
            Logger target = Logger.getInstance();
            string loginfo = _writeLog(LogTypes.LOG);
            FileInfo file = new FileInfo(loginfo);
            DateTime dt = file.LastWriteTime;
            string msg = "[123][456] test case LogTest";
            target.Log(msg);
            Logger.Distroy();
            file = new FileInfo(loginfo);
            Assert.IsTrue(dt < file.LastWriteTime);
        }

        [TestMethod()]
        public void WriteMsgInMutiThreadTest()
        {
            Logger target = Logger.getInstance();
            MethodInfo info = target.GetType().GetMethod(LogTypes.DEBUG);
            Attribute[] attr = LogAttribute.GetCustomAttributes(info);
            LogAttribute loginfo = attr[0] as LogAttribute;
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(() =>
                {
                    FileInfo file = new FileInfo(loginfo.LogFile);
                    DateTime dt = file.LastWriteTime;
                    string msg = "test case WriteMsgInMutilThread write : " + Convert.ToString((int)(System.DateTime.Now.ToFileTimeUtc() % 10000));
                    target.Debug(msg);
                    file = new FileInfo(loginfo.LogFile);
                    Assert.IsTrue(dt < file.LastWriteTime);
                });
                thread.Start();
            }
            Logger.Distroy();
        }
        private string _writeLog(string log_type) {

            Logger target = Logger.getInstance();
            MethodInfo info = target.GetType().GetMethod(log_type);
            Attribute[] attr = LogAttribute.GetCustomAttributes(info);
            LogAttribute loginfo = attr[0] as LogAttribute;
            return loginfo.LogFile;
        }
    }
}
