using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Common
{
    public class ApiHelper
    {
        private static string GenerateTimeStamp()
        {
           return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }
        private static string ToBase64Encode(string value)
        {
            byte[] inArray = new byte[value.Length];
            inArray = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(inArray);
        }
        private static string HashThis(string inputValue, string salt)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string s = inputValue.ToLower(CultureInfo.CurrentCulture) + salt;
            using (SHA256Managed sHA256Managed = new SHA256Managed())
            {
                byte[] array = sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetByteCount(s));
                byte[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    byte b = array2[i];
                    stringBuilder.Append(b.ToString("x2", CultureInfo.CurrentCulture));
                }
            }
            return ToBase64Encode(stringBuilder.ToString());
        }

        public static string GenerateAuthenticationHeader(string requestType, string uri, string appId, string sourceId, string salt)
        {
            string authHeader = string.Empty;
            string timeStamp = GenerateTimeStamp();
            string hashKey = string.Concat(new object[]
            {
                appId,
                sourceId,
                requestType,
                uri,
                timeStamp
            });

            authHeader = string.Format("osnAuth osnauth_x_application_id={0},  osnauth_x_source_id={1}, osnauth_x_timestamp={2}, osnauth_x_signature={3}", new object[]
            {
                appId,
                sourceId,
                timeStamp,
                HashThis(hashKey,salt)
            });
            return authHeader;
        }
    

    }
}
