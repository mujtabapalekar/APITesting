using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Dapper;
using System.Collections;
using System.Data;
using System.Collections.ObjectModel;
using APITesting.Activity;
using APITesting.Entities;

namespace APITesting.DataAccess
{
    class DataAccess
    {

        public static string ExcelFileConnection(string excelFileEnvVar)
        {
            var filePath = ConfigurationManager.AppSettings[excelFileEnvVar];
            var con = string.Format(@"Provider= Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", filePath);
            return con;
        }

        public static Collection<UserData> GetTestData(string excelFileEnvVar,string query)
        {
            Collection<UserData> userDataCollection = new Collection<UserData>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();
                query = string.Format(query, "Y");
                IEnumerable<UserData> values = connection.Query<UserData>(query);
                foreach (UserData item in values)
                {
                    userDataCollection.Add(item);
                }
                connection.Close();
                return userDataCollection;
            }            
        }

        public static Collection<TestsToRunDetails> GetTestCasesToExecute()
        {
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestsToRunDetails> TestCasesCollection = new Collection<TestsToRunDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();
                
                var query = string.Format("select DISTINCT TcId,TcName from [DataSet$] where RunFlag='{0}'", "Y");
                IEnumerable<TestsToRunDetails> values = connection.Query<TestsToRunDetails>(query, "Y");
                foreach (TestsToRunDetails item in values)
                {
                    TestCasesCollection.Add(item);
                }
                connection.Close();
                return TestCasesCollection;
            }
        }
        public static Collection<TestStepDetails> GetStepsToExecute(string tcId)
        {
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestStepDetails> TestStepsCollection = new Collection<TestStepDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();

                var query = string.Format("select * from [DataSet$] where TcId='{0}'", tcId);
                IEnumerable<TestStepDetails> values = connection.Query<TestStepDetails>(query, "Y");
                foreach (TestStepDetails item in values)
                {
                    TestStepsCollection.Add(item);
                }
                connection.Close();
                return TestStepsCollection;
            }
        }
        
        public static Collection<TestDataDetails> GetTestDataForStep(string tcId)
        {
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestDataDetails> TestDataCollection = new Collection<TestDataDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();

                var query = string.Format("select * from [DataSet$] where TcId='{0}'", tcId);
                IEnumerable<TestDataDetails> values = connection.Query<TestDataDetails>(query, "Y");
                foreach (TestDataDetails item in values)
                {
                    TestDataCollection.Add(item);
                }
                connection.Close();
                return TestDataCollection;
            }
        }
        public static Collection<UserData> GetTestDataWithCommand(string excelFileEnvVar)
        {
            Collection<UserData> userDataCollection = new Collection<UserData>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();
                var query = string.Format("select * from [DataSet$] where RunFlag='{0}'", "Y");
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    using (OleDbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            userDataCollection.Add(new UserData()
                            {
                                BaseURL = (dr["BaseURL"] != DBNull.Value) ? dr["BaseURL"].ToString() : string.Empty,
                            });
                        }
                    }
                };
                connection.Close();
                return userDataCollection;
            }
        }




    

    }
}
