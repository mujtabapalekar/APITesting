using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.QueryDb
{
    public class AccessDynamoDb
    {
        SessionAWSCredentials credentials = new Amazon.Runtime.SessionAWSCredentials("ASIA4575XKP4T3TX5C7Q",
"x6oM2WUFgvvfWhaz1Uzu1jCkLEqQ9eSCveT14DBb",
"FQoGZXIvYXdzEKT//////////wEaDO0SR0TSJpwEcYAs1yLrAbQ3xyy qxilDwWFA9rqIQtqQSEQ / Na + JE6Lv5oNy1mBzv / SWbuTSPrdn47UV5gmrTbPLicCvf2Db + OaagGfMV15 8klOrq7ic86ObSVxWmtPSm7aKhbLuLEDpDFnMdVZ01Amui + pGtoXPwscqjaXzinKc9YrJaM4lZZ0YI50 gleifgcyZmW9e84UmbxnJqqSRBvR1Qk0AQaFPxl9HY4U0vdIJYJstGtAfBQNSSXAOXMumBc0suSSzQNT O6FE / xspGOlVWEGVDpbeUTQA4niGNJ7OvTqCKLfTiOQ0P6OxwmnVv02EXRbtq7V15viEooZKs6QU =");

        AmazonDynamoDBClient DbClient;

        public AccessDynamoDb()
        {
            DbClient = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.EUWest1);
        }

        public static Task<QueryResponse> QueryDynamoDb(string tableName, Dictionary<string,Condition> conditions)
        {

            QueryRequest scanRequest = new QueryRequest { TableName = tableName, KeyConditions = conditions };
            return DbClient.QueryAsync(scanRequest);
        }

    }
}
