using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace APITesting.Steps
{
    [Binding]

    public sealed class MspAwsApi
    {
        [Given(@"I have provided an endpoint (.*)")]
        public void GivenIHaveProvidedAnEndpoint(string endpoint)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have a base url (.*)")]
        public void GivenIHaveABaseUrlHttpsXflvuc_Execute_Api_Eu_West_Amazonaws_Com(string baseUrl)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I call post method of api")]
        public void WhenICallPostMethodOfApi()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be returned in json")]
        public void ThenTheResultShouldBeReturnedInJson()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
