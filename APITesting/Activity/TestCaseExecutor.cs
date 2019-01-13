using APITesting.Main;
using APITesting.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITesting.Entities;
using APITesting.Reporting;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace APITesting.Activity
{
    public class TestCaseExecutor
    {
        public Collection<TestDataDetails> AllRows;
        private static readonly object Locker = new object();
        private static TestCaseExecutor DriverScript = null;
        private TestCaseExecutor()
        {
            AllRows = DataAccess.DataAccess.GetAllRows();
        }

        public static TestCaseExecutor getInstance()
        {
            lock (Locker)
            {
                if (DriverScript == null)
                {
                    DriverScript = new TestCaseExecutor();
                }
            }

            return DriverScript;
        }
       


        public void ExecuteTests()
        {
    
            Boolean isPass = true;
            var TestsToRun = DataAccess.DataAccess.GetTestCasesToExecute();
            //TestsToRun.Count();
            foreach (TestsToRunDetails tc in TestsToRun)
            {
                isPass = true;
                Reporter.oReport.CreateTest(tc.TcName);
                var StepsToRun = DataAccess.DataAccess.GetStepsToExecute(tc.primaryKey);
                //TestsToRun.Count();
                foreach (TestStepDetails step in StepsToRun)
                {
                    Reporter.oReport.PassTest("Execution started for step: " + step.Action.ToString());
                    if (!StepExecutor.ExecuteStep(step))
                    {
                        isPass = false;
                        Reporter.oReport.FailTest("Test Failed at Step " + step.Action.ToString() + " Exiting Test Case");
                        break;
                    }
                }
                if (isPass == true)
                {
                    Reporter.oReport.PassTest("Test " + tc.TcName.ToString() + " executed successfully.");
                }
                else
                {
                    Reporter.oReport.FailTest("TC " + tc.TcName.ToString() + " execution failed.");
                }

            }
            Reporter.oReport.FlushReport();
        }


    }
}
