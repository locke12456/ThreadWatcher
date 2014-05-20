using libUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace libUtilitiesTests
{
    
    
    /// <summary>
    ///This is a test class for JavaScriptEngineTest and is intended
    ///to contain all JavaScriptEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JavaScriptEngineTest
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
            JavaScriptEngine target = JavaScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
            string filename = "..\\..\\Test file\\Scripts\\parmetetizes.js"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            object actual;
            Dictionary<string,object> list = new Dictionary<string,object>()
            {{"a","b"},{"c","d"}};
            target.Parameters.Add("args", list);
            actual = target.RunScript(filename);
            Assert.AreEqual(expected, (bool)actual);
           // Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Parameters
        ///</summary>
        //[TestMethod()]
        //public void ParametersTest()
        //{
        //    JavaScriptEngine target = JavaScriptEngine.getInstance(); // TODO: Initialize to an appropriate value
        //    Dictionary<string, object> expected = null; // TODO: Initialize to an appropriate value
        //    Dictionary<string, object> actual;
        //    target.Parameters = expected;
        //    actual = target.Parameters;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for getInstance
        ///</summary>
        //[TestMethod()]
        //public void getInstanceTest()
        //{
        //    JavaScriptEngine expected = null; // TODO: Initialize to an appropriate value
        //    JavaScriptEngine actual;
        //    actual = JavaScriptEngine.getInstance();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
