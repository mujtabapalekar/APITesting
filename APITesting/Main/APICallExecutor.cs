using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using NUnit.Framework;
using Osn.Ott.Api.UI.Model.Subscription;
using APITesting.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using APITesting.Process;
using APITesting.Entities;

namespace APITesting.Main
{
    public static class APICallExecutor
    {

        public static void MakeApiCall()
        {

            
            //string json = JsonConvert.SerializeObject(tmp);
            var client = new RestClient();
            client.BaseUrl = new Uri("https://jsonplaceholder.typicode.com/posts");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest("/2", Method.GET,DataFormat.Json);
            //request.Resource = "statuses/friends_timeline.xml";
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.StatusCode+ ":    " + response.Content);
            Assert.AreEqual(200,(int)response.StatusCode,"Status code is not 200");
        }

        public static void MakeApiCreatePostCall()
        {
            object tmp = new
            {
                userid = 1,
                title = "asddas",
                bb = "asdda"
            };
            string json = SimpleJson.SimpleJson.SerializeObject(tmp);
            var client = new RestClient();
            client.BaseUrl = new Uri("https://jsonplaceholder.typicode.com/posts    ");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest(Method.POST);
            //request.Resource = "/2";
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //SimpleJson.SimpleJson.
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.StatusCode + ":    " + response.Content);
            Assert.AreEqual(201, (int)response.StatusCode, "Status code is not 201");
        }

        public static void MakeApiUpdatePutCall()
        {
            object tmp = new
            {
                id=2,
                userid = 1,
                title = "asddas",
                bb = "asdda"
            };
            string json = SimpleJson.SimpleJson.SerializeObject(tmp);
            var client = new RestClient();
            client.BaseUrl = new Uri("https://jsonplaceholder.typicode.com/posts    ");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest(Method.PUT);
            request.Resource = "/2";
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //SimpleJson.SimpleJson.
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.StatusCode + ":    " + response.Content);
        }
        public static bool PostAWSMSPCreateCall(TestDataDetails userData1)
        {
            Boolean isPass = true;
            try
            {
                //var userData1 = DataAccess.DataAccess.GetTestData("TestDataSheetPath", "select * from [DataSet$] where RunFlag='{0}'").FirstOrDefault();
                string avc;
                string valueToFind;
                //Call to dynamically build query based on parameters passed
                //var dynamicReq=RequestBuilder.BuildRequestBody(userData1.Body);             
                //string json = SimpleJson.SimpleJson.SerializeObject(dynamicReq);

                var client = new RestClient();
                client.BaseUrl = new Uri(userData1.BaseUrl);
                //client.Authenticator = new HttpBasicAuthenticator("username", "password");
                var request = new RestRequest(Method.POST);
                request.Resource = userData1.Resources;
                request.AddHeader("Authorization", "osnAuth osnauth_x_application_id=6,  osnauth_x_source_id=14, osnauth_x_timestamp=1546933214, osnauth_x_signature=ZWRjMDk2ZTI0ODZiNTkzZGQ4OWI5ZDVkNjA1OTE1MDYwNTU1MTg0ZGE3ZDE5MjgxZWQ0MDA4YTRjOTU3YjYwMw==");
                request.AddParameter("application/json", userData1.Body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                avc = response.Content.ToString();
                valueToFind = avc.Between("customerType\":\"", "\",");
                Debug.WriteLine("Validating Respose body attributes... Params to check: " + userData1.Validation + "\n Response body: " + response.Content.ToString());
                if (!DataAccess.DataAccess.UpdateExcelUsingNpoi(response.Content.ToString(), Int32.Parse(userData1.Key), "ApiResponse"))
                    //DataAccess.DataAccess.UpdateExcelUsingNpoi(response.Content.ToString(), Int32.Parse(userData1.Key), "ApiResponse");
                {
                    isPass = false;
                };
                Reporting.Reporter.oReport.PassTest("API Executed. Response: "+ response.Content.ToString());
                //Moved to ResponseValidation class
                //Assert.IsTrue(ResponseValidation.ValidateAttributeSet(userData1.Validation, response.Content.ToString()));
                Debug.WriteLine((int)response.StatusCode + " : " + response.StatusCode + ":    " + response.Content);
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not 201");
            }
            catch (Exception e)
            {

                isPass=false;
                Reporting.Reporter.oReport.FailTest("APICallExecution failed with following error: \n" + e.ToString());
            }
            return isPass;        
        }

        public static void PostAWSMSPCreateCallBackup()
        {
            var userData1 = DataAccess.DataAccess.GetTestData("TestDataSheetPath", "select * from [DataSet$] where RunFlag='{0}'");
            
            Dictionary<string, string> oExtra = new Dictionary<string, string>();
            oExtra.Add("MCC", "965");
            oExtra.Add("MNC", "01");
            oExtra.Add("Prod", "01");
            CreateRequest newReq = new CreateRequest();
            newReq.UserId = "00212029-ba97-468f-b670-b21eb2a93a8e";
            newReq.EmailAddress = "info@osn.com";
            newReq.MobileNumber = "96558880449033";
            newReq.Packages = (new List<int>() { 3507 }).ToArray();
            newReq.BirthDate = null;
            newReq.Address = null;
            newReq.Country = null;
            newReq.City = null;
            newReq.Gender = null;
            newReq.CustomerUsernameID = null;
            newReq.Password = "1234567890";
            newReq.Name = null;
            newReq.Title = null;
            newReq.LanguagePreference = null;
            newReq.Email2 = null;
            newReq.Mobile2 = null;
            newReq.Extra = oExtra;
            //newReq.CreatedDate = DateTime.Now.Date;
            //newReq.ExpiryDate = "2023-12-12T13:00:54.415093Z";
            object tmp = new
            {
                userid = 1,
                title = "asddas",
                bb = "asdda"
            };
            string json = SimpleJson.SimpleJson.SerializeObject(newReq);
            var client = new RestClient();
            client.BaseUrl = new Uri("https://xf0lv66uc8.execute-api.eu-west-1.amazonaws.com");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest(Method.POST);
            request.Resource = "/Dev/create";
            request.AddHeader("Authorization", "osnAuth osnauth_x_application_id=6,  osnauth_x_source_id=14, osnauth_x_timestamp=1546887932, osnauth_x_signature=NDY3ODM3NmMwYzVkNGUyMDM1ZmYxMjc2NDVmYmFlMWI3ZWM5ODc1MTdkYzkwMTVmNDQ5MzBjMTA2ZTIyYWVjMw==");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //SimpleJson.SimpleJson.
            IRestResponse response = client.Execute(request);

            Debug.WriteLine((int)response.StatusCode + " : " + response.StatusCode + ":    " + response.Content);
            Assert.AreEqual(200, (int)response.StatusCode, "Status code is not 201");

        }



    }
}
