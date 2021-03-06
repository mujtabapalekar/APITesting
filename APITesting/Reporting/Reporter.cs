﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace APITesting.Reporting
{
    public class Reporter
    {
        private ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public ExtentTest test;
        public string stepName;
        public string baseDirectory;
        public static Reporter oReport = new Reporter();
        private Reporter()
        {
            //Setting up extent report

            string reportPath = "C:\\Workspace\\TestResults\\APITesting\\NeulionMspIntegration\\NeulionMSPReport-" + DateTime.Now.ToString("ddMMyyyy - HHmm") + ".html";            
            htmlReporter = new ExtentHtmlReporter(@reportPath);
            htmlReporter.Configuration().Theme = Theme.Standard;
            htmlReporter.Configuration().DocumentTitle = "Automation test report";
            htmlReporter.Configuration().ReportName = "Automation test execution";
            htmlReporter.Configuration().JS = "$('.brand-logo').text('text').append('<img src=C:\\Workspace\\TestResults\\UnifyCVM\\osn-logo.png>')";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            //For screenshot
            baseDirectory = "C:\\Workspace\\Automation\\Projects\\UNIFY_CRB_CVM\\Unify_CRB_CVM_Selenium\\FW_Unify_CRB_CVM\\Tests_Unify_CRB_CVM\\Results\\Screenshots\\";

        }

        public void CreateTest(string TCName)
        {
            test = extent.CreateTest(TCName);
        }

        public void PassTest(string StepDescription)
        {
            test.Pass(StepDescription);
        }

        public void FailTest(string StepDescription)
        {
            test.Fail(StepDescription);
        }

        public void FlushReport()
        {
            extent.Flush();
        }

    }
}
