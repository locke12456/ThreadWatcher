using libUtilities;
using libWatcherDialog.DebugScriptEngine;
using libWatcherDialog.DebugScriptEngine.Breakpoint;
using libWatcherDialog.PropertyItem.DebugScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace libWatcherDialogTest
{


    /// <summary>
    ///This is a test class for DebugScriptEngineTest and is intended
    ///to contain all DebugScriptEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DebugScriptEngineTest
    {

        private Search search = new Search(new System.Collections.Generic.List<string>() { "..\\..\\Test file\\" });
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for RunScript
        ///</summary>
        [TestMethod()]
        public void RunScriptTest_breakpointInfo()
        {
            DebugScriptEngine target = DebugScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
            // string filename = "..\\..\\Test file\\Scripts\\breakpoint_info.js"; 
            string filename = search.GetFile("list.js").FullName;// TODO: Initialize to an appropriate value
            object actual;
            actual = target.RunScript(filename);
            DebugScriptItem info = (actual as BreakpointRule).GenerateScriptItem();
            
            filename = search.GetFile(info.BreakpointInfo.name).FullName;
            string code = File.ReadAllLines(filename)[info.BreakpointInfo.line-1];
            int index = code.IndexOf(info.BreakpointInfo.code);
            Assert.IsTrue(DebugScriptsManagement.getInstance().GetList().Count > 0);
            Assert.AreEqual(index, 1);
            Assert.IsNotNull(actual);
            // Assert.Inconclusive("Verify the correctness of this test method.");
        }
        /// <summary>
        ///A test for RunScript
        ///</summary>
        [TestMethod()]
        public void RunScriptTest_virtual_variable()
        {
            DebugScriptEngine target = DebugScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
            //string filename = "..\\..\\Test file\\Scripts\\virtual_variable.js"; // TODO: Initialize to an appropriate value
            string filename = search.GetFile("virtual_variable.js").FullName;
            object actual;
            actual = target.RunScript(filename);
            Assert.IsNotNull(actual);
            // Assert.Inconclusive("Verify the correctness of this test method.");
        }
        /// <summary>
        ///A test for RunScript
        ///</summary>
        [TestMethod()]
        public void RunScriptTest_condition()
        {
            DebugScriptEngine target = DebugScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
            //string filename = "..\\..\\Test file\\Scripts\\condition.js"; // TODO: Initialize to an appropriate value
            string filename = search.GetFile("condition.js").FullName;
            object actual;

            actual = target.RunScript(filename);
            Assert.IsNotNull(actual);
            // Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
