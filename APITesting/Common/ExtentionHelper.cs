using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITesting.Entities;

namespace APITesting.Process
{
    internal static class ExtentionHelper
    {
        public static string Between(this string src, string findfrom, string findto)
        {
            //change to check with ignorecase
            int start = src.IndexOf(findfrom,StringComparison.OrdinalIgnoreCase);
            int to = src.IndexOf(findto, start + findfrom.Length, StringComparison.OrdinalIgnoreCase);

            //Handing new line character in case of findto
            if(to<0)
            {
                findto = "\",";
                to = src.IndexOf(findto, start + findfrom.Length);
            }
            if (start < 0 || to < 0) return "";
            string s = src.Substring(
                           start + findfrom.Length,
                           to - start - findfrom.Length);
            return s;
        }

        public static string RemoveChars(this string str, string[] charsToRemove)
        {           
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            return str;
        }

        internal static int GetRowNumber(Collection<TestDataDetails> allRows, string TcId)
        {
            int comp = -1;
            int rowNoOfExpectedData = 0;
            foreach (TestDataDetails dataRow in allRows)
            {
                rowNoOfExpectedData++;
                comp = string.Compare(TcId, dataRow.TcId);
                if (comp == 0)
                {
                    return rowNoOfExpectedData;
                }
            }
            if (comp == 0 && rowNoOfExpectedData > 0)
            {
                return rowNoOfExpectedData;
            }
            else
            {
                return -1;
            }
        }

        internal static string GetAttributeValueFromJsonFormat(this string actualResponseContent, string attributeToFetch)
        {
            //Removing all spaces to fix an issue while fetching value of the attribute
            actualResponseContent = actualResponseContent.Replace(" ", "");
            string actualAttributeValueFromResponse = actualResponseContent.Between(attributeToFetch + "\":", ",\"");
            if(actualAttributeValueFromResponse=="")
            {
                actualAttributeValueFromResponse = actualResponseContent.Between(attributeToFetch + "\" :", ",\"");
            }
            var charsToRemove = new string[] { "\"", "," };
            actualAttributeValueFromResponse = actualAttributeValueFromResponse.RemoveChars(charsToRemove);
            return actualAttributeValueFromResponse;

        }

    }
}
