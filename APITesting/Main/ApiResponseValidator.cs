using System;

namespace APITesting.Main
{
    internal class ApiResponseValidator
    {
        internal static void ValidateResponse(string expectedAttributeSet,string actualResponseContent)
        {
            ResponseValidation.ValidateAttributeSet(expectedAttributeSet, actualResponseContent);
        }
    }
}