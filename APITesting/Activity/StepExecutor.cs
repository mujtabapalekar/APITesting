using APITesting.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace APITesting.Activity
{
    internal class StepExecutor
    {
        
        internal static Boolean ExecuteStep(TestStepDetails step)
        {            
            Boolean isPass = true;            
            var testData = DataAccess.DataAccess.GetTestDataForStep(step.TcId).FirstOrDefault();         
            switch (testData.Action)
            {
                case "RunAPIRequest":
                    Main.APICallExecutor ApiExecutor = new Main.APICallExecutor();
                    if (!ApiExecutor.PostAWSMSPCreateCall(testData))
                    {
                        isPass = false;
                    }
                break;
                case "ValidateApiResponse":
                    if (!Main.ResponseValidation.FetchDataAndValidateAttributeSet(testData.DataToValidate, testData.fieldsToValidate))
                    {
                        isPass = false;
                    }
                    break;
                default:
                    isPass = false;
                    break;
            }

            return isPass;

        }
    }
}