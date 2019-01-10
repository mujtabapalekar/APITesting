using APITesting.Process;
using System;
using System.Linq;

namespace APITesting.Main
{
    internal class ResponseValidation
    {

        
        private static bool ValidateAttributeAgainstResponse(string expectedAttribute, string actualResponseContent)
        {
            string[] expectedAttributeParams = expectedAttribute.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            //Fetching the Attribute to validate from response
            string actualAttributeFromResponse=actualResponseContent.Between(expectedAttributeParams[0]+"\":", ",\"");
            var charsToRemove = new string[] { "\"", "," };
            actualAttributeFromResponse = actualAttributeFromResponse.RemoveChars(charsToRemove);
            //Evaluating the attribute against requested validation
            int comp=1;
            switch (expectedAttributeParams[1])
            {
                case "=":
                    comp=string.Compare(actualAttributeFromResponse, expectedAttributeParams[2]);
                    break;

                case "NULL":
                    comp=string.Compare(actualAttributeFromResponse, "null");
                    break;
                case "NOTEMPTY":
                    if (string.IsNullOrEmpty(actualAttributeFromResponse) || string.IsNullOrWhiteSpace("") || string.Compare("", "null") == 0)
                    {
                        comp = 0;
                    }
                    break;
                default:
                    comp = 1;
                    break;
            }

            if (comp==0)
            {
                return true;
            }
            else
            {
                return false;
            }         

        }

        internal static bool FetchDataAndValidateAttributeSet(string dataLocator, string fieldsToValidate)
        {
            bool isPass = true;
            string responseToValidate = "";
            string[] fetchDataKeyVal = dataLocator.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var testData = DataAccess.DataAccess.GetDataToValidateParam(fetchDataKeyVal[0]).FirstOrDefault();
            switch (fetchDataKeyVal[1])
            {
                case "ApiResponse":
                    responseToValidate = testData.ApiResponse;
                    break;
                default:
                    break;
            }


            if (!ValidateAttributeSet(fieldsToValidate, responseToValidate))
            {
                isPass = false;
                Reporting.Reporter.oReport.FailTest("Validation for API request failed. Expected data: " + fieldsToValidate + "\n Actual response: " + responseToValidate);
            }
            else {
            Reporting.Reporter.oReport.PassTest("Validation for API request successful. Expected data: " + fieldsToValidate + "\n Actual response: " + responseToValidate);
            }
            return isPass;
            
        }

        internal static bool ValidateAttributeSet(string expectedAttributeSet, string actualResponseContent)
        {
            string[] splitedAttributes = expectedAttributeSet.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            bool isComparisonSuccess=true;
            foreach (string s in splitedAttributes)
            {
                //string sval= s[0].ToString();
                //actualResponseContent.Between(,)
                if (!ValidateAttributeAgainstResponse(s, actualResponseContent))
                {
                    isComparisonSuccess = false;
                }               
            }
            return isComparisonSuccess;
        }
    }
}