using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Entities
{

    class TestModule
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string RunFlag { get; set; }
    }

    class TestsToRunDetails
    {
        public string TcId { get; set; }
        public string TcName { get; set; }
        public string primaryKey { get
            {
                string[] splitTcId = TcId.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                return splitTcId[0];
            } }
        /*
        public string RunFlag { get; set; }
        public string TcName { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        */

    }


    class TestRunnerDetails
    {
        public string TcId { get; set; }
        public string TcName { get; set; }
        public string Description { get; set; }
        public string RunFlag { get; set; }
        public string Resources { get; set; }
        public string Parameter { get; set; }
        public string DataModel { get; set; }
        public string Body { get; set; }
        public string fieldsToValidate { get; set; }

    }

    class TestStepDetails
    {
        //public string Key { get; set; }
        public string TcId { get; set; }
        public string RunFlag { get; set; }
        public string TcName { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        /*
        public string RequestMethod { get; set; }
        public string BaseUrl { get; set; }
        public string Resources { get; set; }
        public string Parameter { get; set; }
        public string DataModel { get; set; }
        public string Body { get; set; }
        public string Validation { get; set; }
        */
    }

}
