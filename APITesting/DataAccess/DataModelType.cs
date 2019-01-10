using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.DataAccess
{
    class DataModelType
    {
        internal enum DataModelReturnType
        {
            TestModule,
            TestsToRunDetails,
            TestRunnerDetails,
            TestStepDetails,
            UserData,
            None

        }

        internal class DataTypeDefinition
        {
            public DataModelReturnType Type { get; set; }
            public string Value { get; set; }
        }

    }
}
