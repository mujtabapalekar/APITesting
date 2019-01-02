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

namespace APITesting.Activity
{
    public static class TestCaseExecutor
    {
        public static void ExecuteTests()
        {
            Boolean isPass = true;
            var TestsToRun = DataAccess.DataAccess.GetTestCasesToExecute();
            TestsToRun.Count();
            foreach(TestsToRunDetails tc in TestsToRun)
            {
                Reporter.oReport.CreateTest(tc.TcName);
                var StepsToRun = DataAccess.DataAccess.GetStepsToExecute(tc.TcId);                
                TestsToRun.Count();
                foreach(TestStepDetails step in StepsToRun)
                {
                    Reporter.oReport.PassTest("Execution started for step <" + step.Action + ">");
                    if (!StepExecutor.ExecuteStep(step))
                    {
                        isPass = false;
                        Reporter.oReport.FailTest("Test Failed at Step <" + step.Action + "> Exiting Test Case");
                        break;
                    }                    
                }
                if(isPass==true)
                {
                    Reporter.oReport.PassTest("Test <" + tc.TcName + "> executed successfully.");
                }
                else
                {
                    Reporter.oReport.PassTest("TC <" + tc.TcName + "> execution failed.");
                }
                
            }
            Reporter.oReport.FlushReport();
        }
        

    }
}
