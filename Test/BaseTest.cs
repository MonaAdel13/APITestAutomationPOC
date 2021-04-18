using NeuroLIM_APITesing.Common;
using NeuroLIM_APITesing.Requests;
using NeuroLIM_APITesing.Responses;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLIM_APITesing.Test
{
    class BaseTest
    {
        protected string currentContextToken;
        protected string refereshToken;

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
        }
    }
}
