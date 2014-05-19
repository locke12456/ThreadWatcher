using libUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace libUtilitiesTests
{
    
    
    /// <summary>
    ///This is a test class for SearchTest and is intended
    ///to contain all SearchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SearchTest
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
        ///A test for GetFile
        ///</summary>
        [TestMethod()]
        public void GetFileTest()
        {
            List<string> paths = new List<string>() { "..\\..\\"}; // TODO: Initialize to an appropriate value
            Search target = new Search(paths); // TODO: Initialize to an appropriate value
            string filename = "parmetetizes.js"; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            FileInfo expected = new FileInfo("..\\..\\Test file\\Scripts\\parmetetizes.js"); // TODO: Initialize to an appropriate value
            FileInfo actual;
            actual = target.GetFile(filename, count);
            Assert.AreEqual(expected.FullName, actual.FullName);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
