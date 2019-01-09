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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Diagnostics;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;

namespace APITesting.DataAccess
{
    class DataAccess
    {

        public static string ExcelFileUpdateConnection(string excelFileEnvVar)
        {
            var filePath = ConfigurationManager.AppSettings[excelFileEnvVar];
            var con = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = {0}; Extended Properties='Excel 8.0;HDR=Yes;'", filePath);
            return con;
        }
        public static string ExcelFileConnection(string excelFileEnvVar)
        {
            var filePath = ConfigurationManager.AppSettings[excelFileEnvVar];
            var con = string.Format(@"Provider= Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", filePath);
            return con;
        }
        public static Collection<UserData> GetTestData(string excelFileEnvVar, string query)
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

        public static Collection<TestDataDetails> GetDataToValidateParam(string rowToCheck)
        {
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestDataDetails> TestDataCollection = new Collection<TestDataDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();

                var query = string.Format("select * from [DataSet$] where Key={0}", rowToCheck);
                IEnumerable<TestDataDetails> values = connection.Query<TestDataDetails>(query);
                foreach (TestDataDetails item in values)
                {
                    TestDataCollection.Add(item);
                }
                connection.Close();
                return TestDataCollection;
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

                var query = string.Format("select * from [DataSet$] where Key={0}", tcId);
                IEnumerable<TestDataDetails> values = connection.Query<TestDataDetails>(query, "Y");
                foreach (TestDataDetails item in values)
                {
                    TestDataCollection.Add(item);
                }
                connection.Close();
                return TestDataCollection;
            }
        }

        public static Boolean UpdateApiResponse(string key, string apiResponse)
        {
            Boolean isPass = true;
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestDataDetails> TestDataCollection = new Collection<TestDataDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                // command.CommandText = "Update [DataSet$] SET ApiResponse=? ";
                //command.CommandText = string.Format("select * from [DataSet$] where RunFlag='{0}'", "Y");
                command.CommandText = string.Format("Update [DataSet$] SET ApiResponse='{0}' WHERE Key={1}", apiResponse, key);
                //  command.Parameters.AddWithValue("ApiResponse",apiResponse);
                if (connection.State == ConnectionState.Open)
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        Reporting.Reporter.oReport.PassTest("Api Response<\n" + apiResponse + "\n> updated to record: " + key);

                    }
                    catch (Exception e)
                    {
                        isPass = false;
                        Reporting.Reporter.oReport.PassTest("Failed to update Api Response with following error: " + e);

                    }

                }
                //var query = string.Format("Update [DataSet$] SET ApiResponse='"+apiResponse+"' where Key='"+key+"'");
                //connection.Execute(query);
                /*IEnumerable<TestDataDetails> values = connection.Query<TestDataDetails>(query, "Y");
                foreach (TestDataDetails item in values)
                {
                    TestDataCollection.Add(item);
                }*/
                connection.Close();
                return isPass;
            }
        }


        public static Boolean UpdateExcelUsingNpoi(string inputText, int row, string columnName)
        {
            Boolean isPass = true;
            string excelFileEnvVar = "TestDataSheetPath";
            try
            {
                var excelPath = ConfigurationManager.AppSettings[excelFileEnvVar];
                //string excelPath = @"C://Workspace/Automation/Projects/APITesting/APITesting/DataAccess/TestData.xlsx";
                int colNoToUpdate = -1;
                // read the workbook
                XSSFWorkbook wb;
                using (FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                }

                // make changes
                ISheet sheet = wb.GetSheetAt(0);

                for (int rowCntr = 0; rowCntr <= sheet.LastRowNum; row++)
                {
                    IRow row1 = sheet.GetRow(rowCntr);
                    if (sheet.GetRow(rowCntr) != null)
                    {
                        foreach (ICell cell in row1.Cells)
                        {
                            if (cell.StringCellValue == columnName)
                            {
                                colNoToUpdate = cell.ColumnIndex;
                                break;
                            }
                        }
                    }
                    break;
                }
                IRow rowToUpdate = sheet.GetRow(row) ?? sheet.CreateRow(row);
                ICell colToUpdate = rowToUpdate.GetCell(colNoToUpdate) ?? rowToUpdate.CreateCell(colNoToUpdate);
                colToUpdate.SetCellValue(inputText);
                // overwrite the workbook using a new stream
                using (FileStream fs = new FileStream(excelPath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fs);
                }
            }
            catch (Exception e)
            {

                isPass = false;
                Reporting.Reporter.oReport.FailTest("Filed to update response into datasheet with following error: " + e);
            }
            return isPass;
        }


        public static void UpdateExcelUsingEpPlus(string filePath, string value, int row, int col)
        {
            //filePath = @"C://Workspace/Automation/Projects/APITesting/APITesting/DataAccess/TestData.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.ElementAt(0);
                //ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                excelWorksheet.Cells[row, col].Value = value;
                //excelWorksheet.Cells[2, 2].Value = "Test2";
                //excelWorksheet.Cells[3, 2].Value = "Test3";

                excelPackage.Save();
            }
            
        }

        public static Boolean UpdateApiResponseUsingAdapter(string key, string apiResponse)
        {
            Boolean isPass = true;
            string excelFileEnvVar = "TestDataSheetPath";
            Collection<TestDataDetails> TestDataCollection = new Collection<TestDataDetails>();
            using (var connection = new OleDbConnection(ExcelFileConnection(excelFileEnvVar)))
            {

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                // command.CommandText = "Update [DataSet$] SET ApiResponse=? ";
                //command.CommandText = string.Format("select * from [DataSet$] where RunFlag='{0}'", "Y");
                command.CommandText = string.Format("SELECT * FROM [DataSet$]");
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);


                //**Insert commands
                adapter.InsertCommand = new OleDbCommand("Insert into [DataSet$] (Key,TcId,RunFlag,TcName,Description,Action,RequestMethod,BaseUrl,Resources                    ,Parameter, Body ,DataModel, ApiResponse, DataToValidate, Validation) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", connection);
                adapter.InsertCommand.Parameters.Add("@Key", OleDbType.Integer, 255).SourceColumn = "Key";
                adapter.InsertCommand.Parameters.Add("@TcId", OleDbType.Char, 255).SourceColumn = "TcId";
                adapter.InsertCommand.Parameters.Add("@RunFlag", OleDbType.Char, 255).SourceColumn = "RunFlag";
                adapter.InsertCommand.Parameters.Add("@TcName", OleDbType.Char, 255).SourceColumn = "TcName";
                adapter.InsertCommand.Parameters.Add("@Description", OleDbType.Char, 255).SourceColumn = "Description";
                adapter.InsertCommand.Parameters.Add("@Action", OleDbType.Char, 255).SourceColumn = "Action";
                adapter.InsertCommand.Parameters.Add("@RequestMethod", OleDbType.Char, 255).SourceColumn = "RequestMethod";
                adapter.InsertCommand.Parameters.Add("@BaseUrl", OleDbType.Char, 255).SourceColumn = "BaseUrl";
                adapter.InsertCommand.Parameters.Add("@Resources", OleDbType.Char, 255).SourceColumn = "Resources";
                adapter.InsertCommand.Parameters.Add("@Parameter", OleDbType.Char, 255).SourceColumn = "Parameter";
                adapter.InsertCommand.Parameters.Add("@DataModel", OleDbType.Char, 255).SourceColumn = "DataModel";
                adapter.InsertCommand.Parameters.Add("@Body", OleDbType.Char, 512).SourceColumn = "Body";
                adapter.InsertCommand.Parameters.Add("@ApiResponse", OleDbType.Char, 512).SourceColumn = "ApiResponse";
                adapter.InsertCommand.Parameters.Add("@DataToValidate", OleDbType.Char, 255).SourceColumn = "DataToValidate";
                adapter.InsertCommand.Parameters.Add("@Validation", OleDbType.Char, 512).SourceColumn = "Validation";

                //**Update commands
                //adapter.UpdateCommand = new OleDbCommand(string.Format("Update [DataSet$] SET ApiResponse='{0}' WHERE Key={1}", apiResponse, key), connection);
                adapter.UpdateCommand = new OleDbCommand("UPDATE [DataSet$] SET ApiResponse = ?" +
                                                       " WHERE Key = ?", connection);
                adapter.UpdateCommand.Parameters.Add("@Key", OleDbType.Integer, 255).SourceColumn= "Key";
                adapter.UpdateCommand.Parameters.Add("@TcId", OleDbType.Char, 255).SourceColumn = "TcId";
                adapter.UpdateCommand.Parameters.Add("@RunFlag", OleDbType.Char, 255).SourceColumn = "RunFlag";
                adapter.UpdateCommand.Parameters.Add("@TcName", OleDbType.Char, 255).SourceColumn = "TcName";
                adapter.UpdateCommand.Parameters.Add("@Description", OleDbType.Char, 255).SourceColumn = "Description";
                adapter.UpdateCommand.Parameters.Add("@Action", OleDbType.Char, 255).SourceColumn = "Action";
                adapter.UpdateCommand.Parameters.Add("@RequestMethod", OleDbType.Char, 255).SourceColumn = "RequestMethod";
                adapter.UpdateCommand.Parameters.Add("@BaseUrl", OleDbType.Char, 255).SourceColumn = "BaseUrl";
                adapter.UpdateCommand.Parameters.Add("@Resources", OleDbType.Char, 255).SourceColumn = "Resources";
                adapter.UpdateCommand.Parameters.Add("@Parameter", OleDbType.Char, 255).SourceColumn = "Parameter";
                adapter.UpdateCommand.Parameters.Add("@DataModel", OleDbType.Char, 255).SourceColumn = "DataModel";
                adapter.UpdateCommand.Parameters.Add("@Body", OleDbType.Char, 512).SourceColumn = "Body";
                adapter.UpdateCommand.Parameters.Add("@ApiResponse", OleDbType.Char, 512).SourceColumn = "ApiResponse";
                adapter.UpdateCommand.Parameters.Add("@DataToValidate", OleDbType.Char, 255).SourceColumn = "DataToValidate";
                adapter.UpdateCommand.Parameters.Add("@Validation", OleDbType.Char, 512).SourceColumn = "Validation";
                

                
                adapter.UpdateCommand.Parameters.Add("@OldKey", OleDbType.Integer, 255, "Key").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldTcId", OleDbType.Char, 255, "TcId").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldRunFlag", OleDbType.Char, 255, "RunFlag").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldTcName", OleDbType.Char, 255, "TcName").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldDescription", OleDbType.Char, 255, "Description").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldAction", OleDbType.Char, 255, "Action").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldRequestMethod", OleDbType.Char, 255, "RequestMethod").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldBaseUrl", OleDbType.Char, 255, "BaseUrl").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldResources", OleDbType.Char, 255, "Resources").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldParameter", OleDbType.Char, 255, "Parameter").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldDataModel", OleDbType.Char, 255, "DataModel").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldBody", OleDbType.Char, 512, "Body").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldApiResponse", OleDbType.Char, 512, "ApiResponse").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldDataToValidate", OleDbType.Char, 255, "DataToValidate").SourceVersion = DataRowVersion.Original;
                adapter.UpdateCommand.Parameters.Add("@OldValidation", OleDbType.Char, 512, "Validation").SourceVersion = DataRowVersion.Original;



                //dataSet.Tables[0].Rows[0]["ApiResponse"] = "hhgfhjfhgfhg";
                dataSet.Tables[0].Rows[0]["ApiResponse"] = apiResponse;
                adapter.RowUpdating += Adapter_RowUpdating;
                adapter.RowUpdated += Adapter_RowUpdated;
                adapter.Update(dataSet);
               
                return isPass;
            }
        }

        private static void Adapter_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private static void Adapter_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected  void OnRowUpdating(System.Data.Common.RowUpdatingEventArgs value)
        {

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
