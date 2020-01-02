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
using APITesting.Activity;
using System.Configuration;
using APITesting.Common;

namespace APITesting.Main
{
    public class APICallExecutor
    {
        
        internal bool PostAWSMSPCreateCall(TestDataDetails userData1)
        {
            Boolean isPass = true;
            
            try
            {
                string generatedHeader="";
                if (userData1.Headers == "IB")
                {
                    
                    string appId = ConfigurationManager.AppSettings["AppId"];
                    string sourceId = ConfigurationManager.AppSettings["SourceId"];
                    string hashSalt = ConfigurationManager.AppSettings["HashSalt"];
                    string uri = userData1.BaseUrl + userData1.Resources;
                    generatedHeader = ApiHelper.GenerateAuthenticationHeader(userData1.RequestMethod, uri, appId, sourceId, hashSalt);
                }
                
                //string avc;
                //string valueToFind;
                //Call to dynamically build query based on parameters passed
                //var dynamicReq=RequestBuilder.BuildRequestBody(userData1.Body);             
                //string json = SimpleJson.SimpleJson.SerializeObject(dynamicReq);

                var client = new RestClient();
                client.BaseUrl = new Uri(userData1.BaseUrl);
                //client.Authenticator = new HttpBasicAuthenticator("username", "password");
                var request = new RestRequest(Method.POST);
                request.Resource = userData1.Resources;
                string[] splitedHeaderParams = userData1.Headers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitedHeaderParams[0]!="NA" && splitedHeaderParams[0] != "IB")
                {
                    foreach (string headerParam in splitedHeaderParams)
                    {
                        string[] headerKeyVal = headerParam.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        request.AddHeader(headerKeyVal[0], headerKeyVal[1]);
                    }
                }
                else if (splitedHeaderParams[0] == "IB")
                {
                    request.AddHeader("Authorization", generatedHeader);

                }
                

                //request.AddHeader("Authorization", "osnAuth osnauth_x_application_id=6,  osnauth_x_source_id=14, osnauth_x_timestamp=1546933214, osnauth_x_signature=ZWRjMDk2ZTI0ODZiNTkzZGQ4OWI5ZDVkNjA1OTE1MDYwNTU1MTg0ZGE3ZDE5MjgxZWQ0MDA4YTRjOTU3YjYwMw==");
                request.AddParameter("application/json", userData1.Body, ParameterType.RequestBody);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IRestResponse response = client.Execute(request);
                //avc = response.Content.ToString();
                //valueToFind = avc.Between("customerType\":\"", "\",");
                //Debug.WriteLine("Validating Respose body attributes... Params to check: " + userData1.Validation + "\n Response body: " + response.Content.ToString());

                //Call to fetch row number of the Test step. We need this to write the response into the Excel sheet
                int rowToUpdate=ExtentionHelper.GetRowNumber(TestCaseExecutor.getInstance().AllRows,userData1.TcId);
                if (rowToUpdate < 0)
                {
                    isPass = false;
                    Reporting.Reporter.oReport.FailTest("Unable to fetch row number for Test Step.");
                }
                if (!DataAccess.DataAccess.UpdateExcelUsingNpoi(response.Content.ToString(), rowToUpdate, "ApiResponse"))
                    //DataAccess.DataAccess.UpdateExcelUsingNpoi(response.Content.ToString(), Int32.Parse(userData1.Key), "ApiResponse");
                {
                    isPass = false;
                };
                Reporting.Reporter.oReport.PassTest("API Executed.Request: "+ userData1.Body + " Response: "+ response.Content.ToString());
                //Moved to ResponseValidation class
                //Assert.IsTrue(ResponseValidation.ValidateAttributeSet(userData1.Validation, response.Content.ToString()));
                Debug.WriteLine((int)response.StatusCode + " : " + response.StatusCode + ":    " + response.Content);
                Assert.AreEqual(Int32.Parse(userData1.ApiResponseCode), (int)response.StatusCode, "Status code is not "+ userData1.ApiResponseCode);
            }
            catch (Exception e)
            {
                isPass=false;
                Reporting.Reporter.oReport.FailTest("APICallExecution failed with following error: \n" + e.ToString());
            }
            return isPass;        
        }
        


    }
}
