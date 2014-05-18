using libWatcherDialog.DebugScriptEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace libWatcherDialogTest
{
    
    
    /// <summary>
    ///This is a test class for DebugScriptEngineTest and is intended
    ///to contain all DebugScriptEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DebugScriptEngineTest
    {


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
        public void RunScriptTest()
        {
            DebugScriptEngine target = DebugScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
            string filename = "..\\..\\Test file\\Scripts\\virtual_variable.js"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            object actual;
            
            actual = target.RunScript(filename);
            Assert.AreEqual(expected, (bool)actual);
            // Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
