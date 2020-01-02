using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITesting.Activity;
using System.Collections.ObjectModel;
using APITesting.Common;
using APITesting.QueryDb;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace APITesting
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void GetTestCase()
        {
            //Test comment
            TestCaseExecutor.getInstance().ExecuteTests();

        }

        [TestMethod]
        public void authHeaderTC()
        {
            string requestType = "POST";
            string uri = "https://mspapi-in.it.osn-dev.net/create";
            string appId = "6";
            string sourceId = "14";
            string salt = "-1+0+3*5#?7";
            string generatedHeader=ApiHelper.GenerateAuthenticationHeader(requestType, uri, appId, sourceId, salt);

        }


        [TestMethod]
        public void accessDB()
        {
            Condition condition = new Condition();
            condition.AttributeValueList = new List<AttributeValue>();
            condition.ComparisonOperator = new ComparisonOperator(ComparisonOperator.BETWEEN);
            condition.AttributeValueList.Add(new AttributeValue { S = "Any string" });
            string tableName = "";
            Dictionary<string, Condition> abc = new Dictionary<string, Condition>();
            abc.Add("", condition);
            AccessDynamoDb.QueryDynamoDb(tableName, abc);
            //string generatedHeader = ApiHelper.GenerateAuthenticationHeader(requestType, uri, appId, sourceId, salt);

        }

    }
}
