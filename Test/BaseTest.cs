using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NeuroLIM_APITesing.Common;
using NeuroLIM_APITesing.Requests;
using NeuroLIM_APITesing.Responses;
using NLog;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Test
{
    class BaseTest
    {
        protected string currentContextToken;
        protected string refereshToken;
        private static Logger log = LogManager.GetCurrentClassLogger();
        protected ExtentReports extentReport;
        protected ExtentTest _test;


        [OneTimeSetUp]
        public void SetUp()
        {
            var client = new RestClient(APIRequestConstants.APIBaseURL);
            //var request = new RestRequest("api/reactant-lists?page={PageNumber}&pageSize={PageSize}&ownedReactantListsOnly={OwnedRLOnly}&SortMetadata[0].Field={SortMetaData}&SortMetadata[0].Direction={SortMetaDataDirection}&isActive={ActiveFlage}", Method.GET);
            var request = new RestRequest(APIRequestConstants.AccountLoginURL, Method.POST);

            request.AddJsonBody(new LoginRequestBody()
            {
                UserName = "admin",
                Password = "123@Sta.com"
            });

            var response = client.Execute(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var loginResponse = new JsonDeserializer().Deserialize<LoginResponse>(response);
                this.currentContextToken = loginResponse.AccessToken;
                this.refereshToken = loginResponse.RefreshToken;
            }
        }


        [OneTimeSetUp]
        public void ReportSetup()
        {
            extentReport = new ExtentReports();
            var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            DirectoryInfo di = Directory.CreateDirectory(dir + "\\Reports");
            var htmlReporter = new ExtentHtmlReporter($"{dir}\\Reports\\TestResult.html");
            extentReport.AddSystemInfo("Environment", "SQE");
            extentReport.AddSystemInfo("Test Name", TestContext.CurrentContext.Test.Name);
            extentReport.AttachReporter(htmlReporter);

        }

        [SetUp]
        public void SetUpReport()
        {
            _test = extentReport.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                log.Debug($"TestCase- {TestContext.CurrentContext.Test.Name} -Faild");
                extentReport.AddTestRunnerLogs($"TestCase- {TestContext.CurrentContext.Test.Name} -Faild");
                _test.Fail($"TestCase- {TestContext.CurrentContext.Test.Name} -Faild");
            }
            else
            {
                log.Debug($"TestCase- {TestContext.CurrentContext.Test.Name} - Success");

            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            var client = new RestClient(APIRequestConstants.APIBaseURL);
            //var request = new RestRequest("api/reactant-lists?page={PageNumber}&pageSize={PageSize}&ownedReactantListsOnly={OwnedRLOnly}&SortMetadata[0].Field={SortMetaData}&SortMetadata[0].Direction={SortMetaDataDirection}&isActive={ActiveFlage}", Method.GET);
            var request = new RestRequest(APIRequestConstants.AccountLogoutURL, Method.POST);

            request.AddHeader(APIRequestConstants.AuthorizationHeaderKey, $"Bearer {this.currentContextToken}");

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                this.currentContextToken = null;
            }

            extentReport.Flush();

        }
    }
}
