using APITesting.Entities;
using System;
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
                    if(!Main.APICallExecutor.PostAWSMSPCreateCall())
                    {
                        isPass = false;
                    }
                break;
                case "ValidateApiResponse":
                    if (!Main.ResponseValidation.ValidateAttributeSet(testData.ApiResponse, testData.Validation))
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