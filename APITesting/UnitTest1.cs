using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITesting.Activity;
using System.Collections.ObjectModel;

namespace APITesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {


            //APITesting.Main.APICallExecutor.PostAWSMSPCreateCall();

        }

        [TestMethod]
        public void GetTestCase()
        {
            //Test comment
            TestCaseExecutor.getInstance().ExecuteTests();

        }

        [TestMethod]
        public void UpdateExcelData()
        {
            //string filePath = @"C://Workspace/Automation/Projects/APITesting/APITesting/DataAccess/TestData.xlsx";
            string newPath = @"C://Workspace/Automation/Projects/APITesting/APITesting/DataAccess/TestData - Copy - Copy.xlsx";
            //Test comment
            //DataAccess.DataAccess.UpdateExcel();
            DataAccess.DataAccess.UpdateExcelUsingNpoi("API Dummy Text",1,"ApiResponse");

            //DataAccess.DataAccess.UpdateExcelUsingEpPlus(filePath,"asdads",2,3);

        }

        [TestMethod]
        public void TcToExecute()
        {
            
            var TestsToRun = DataAccess.DataAccess.GetTestCasesToExecute();

            
            int var=TestsToRun.Count;
        }

        
    }
}
