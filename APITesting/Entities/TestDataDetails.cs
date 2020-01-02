using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Entities
{
    public class UserData
    {
        //public string Key { get; set; }
        public string TcName { get; set; }
        public string RequestMethod { get; set; }
        public string BaseURL { get; set; }
        public string Resources { get; set; }
        public string Parameter { get; set; }
        public string DataModel { get; set; }
        public string Body { get; set; }
        public string fieldsToValidate { get; set; }

    }

    public class TestDataDetails
    {
        //public string RecordNo { get; set; }
        //public string Key { get; set; }
        public string TcId { get; set; }
        public string RunFlag { get; set; }
        public string TcName { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public string RequestMethod { get; set; }
        public string BaseUrl { get; set; }
        public string Resources { get; set; }
        public string Parameter { get; set; }
        public string Headers { get; set; }
        public string DataModel { get; set; }
        public string Body { get; set; }
        public string ApiResponse { get; set; }
        public string ApiResponseCode { get; set; }
        public string DataToValidate { get; set; }
        public string fieldsToValidate { get; set; }
    }
}
