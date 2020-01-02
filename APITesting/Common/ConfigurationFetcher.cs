using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Process
{
    public static class ConfigurationFetcher
    {

        internal static string getDataSheetPath()
        {
            var projectName = ConfigurationManager.AppSettings["ProjectName"];
            
            string dataSheetPathKey = "TestDataSheetPath-" + projectName;
            string dataSheetLocator = ConfigurationManager.AppSettings[dataSheetPathKey];
            string relativePath = System.IO.Directory.GetCurrentDirectory();
            //Directory.GetDirectoryRoot.relativePath();
            int dataAccessIndex = relativePath.IndexOf("bin");
            relativePath = relativePath.Remove(dataAccessIndex);
            string dataSheetPath = relativePath + dataSheetLocator;
            return dataSheetPath;
        }

        internal static string getTestResultPath()
        {           
            string dataSheetPath = ConfigurationManager.AppSettings["TestResultPath"];           
            return dataSheetPath;
        }

    }
}
