using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITesting.Activity;

namespace APITesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {


            APITesting.Main.APICallExecutor.PostAWSMSPCreateCall();

        }

        [TestMethod]
        public void GetTestCase()
        {

            TestCaseExecutor.ExecuteTests();

        }
    }
}
