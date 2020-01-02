using System;
using Osn.Ott.Api.UI.Model.Subscription;
using System.Dynamic;
using System.Collections.Generic;

namespace APITesting.Main
{
    internal class RequestBuilder
    {
        internal static ExpandoObject BuildRequestBody(string body)
        {
            //string[] split = body.Split(new string[] { "|", "!", ":" }, StringSplitOptions.RemoveEmptyEntries);
            string[] splitedBodyParams = body.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            dynamic obj = new ExpandoObject();
            foreach(string s in splitedBodyParams)
            {
                string[] splittedindividualParams = s.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                
                switch(splittedindividualParams[1])
                {
                    case "UserId":
                        if (string.Equals(splittedindividualParams[2],"null")) { obj.UserId = null;break; }                        
                        obj.UserId = splittedindividualParams[2];
                        break;
                    case "EmailAddress":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.EmailAddress = null; break; }
                        obj.EmailAddress = splittedindividualParams[2];
                        break;
                    case "MobileNumber":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.MobileNumber = null; break; }
                        obj.MobileNumber = splittedindividualParams[2];
                        break;
                    case "Packages":
                        int[] arrPack = { int.Parse(splittedindividualParams[2].ToString()) };
                        obj.Packages = arrPack;
                        //obj.Packages = s[2];
                        break;
                    case "BirthDate":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.BirthDate = null; break; }
                        obj.BirthDate = splittedindividualParams[2];
                        break;
                    case "Address":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Address = null; break; }
                        obj.Address = splittedindividualParams[2];
                        break;
                    case "Country":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Country = null; break; }
                        obj.Country = splittedindividualParams[2];
                        break;
                    case "City":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.City = null; break; }
                        obj.City = splittedindividualParams[2];
                        break;
                    case "Gender":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Gender = null; break; }
                        obj.Gender = splittedindividualParams[2];
                        break;
                    case "CustomerUsernameID":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.CustomerUsernameID = null; break; }
                        obj.CustomerUsernameID = splittedindividualParams[2];
                        break;
                    case "Password":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Password = null; break; }
                        obj.Password = splittedindividualParams[2];
                        break;
                    case "Name":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Name = null; break; }
                        obj.Name = splittedindividualParams[2];
                        break;
                    case "Title":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Title = null; break; }
                        obj.Title = splittedindividualParams[2];
                        break;
                    case "LanguagePreference":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.LanguagePreference = null; break; }
                        obj.LanguagePreference = splittedindividualParams[2];
                        break;
                    case "Email2":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Email2 = null; break; }
                        obj.Email2 = splittedindividualParams[2];
                        break;
                    case "Mobile2":
                        if (string.Equals(splittedindividualParams[2], "null")) { obj.Mobile2 = null; break; }
                        obj.Mobile2 = splittedindividualParams[2];
                        break;
                    case "Extra":
                        string[] splittedData = splittedindividualParams[2].ToString().Split(new string[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
                        Dictionary<string, string> oExtra = new Dictionary<string, string>();
                        foreach (string d in splittedData)
                        {
                            string[] keyVal = d.Split(new string[] { "~" }, StringSplitOptions.RemoveEmptyEntries);
                            oExtra.Add(keyVal[0], keyVal[1]);                            
                        }
                        obj.Extra = oExtra;
                        break;

                    default:
                        Console.WriteLine("Data type not found to Build Request body");
                        break;

                }

            }

            return obj;
        }
    }
}