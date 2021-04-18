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
    class ReactantListAPITest : BaseTest
    {
        private int createdReactantListID;
        
        private int NewlyCreatedReactantList;

       private Random random = new Random();
        //     [SetUp]
        //        public void SetUp()
        //       {
        //var client = new RestClient("https://services-test.neuro-ace.net");
        ////var request = new RestRequest("api/reactant-lists?page={PageNumber}&pageSize={PageSize}&ownedReactantListsOnly={OwnedRLOnly}&SortMetadata[0].Field={SortMetaData}&SortMetadata[0].Direction={SortMetaDataDirection}&isActive={ActiveFlage}", Method.GET);
        //var request = new RestRequest("api/account/ExchangeToken", Method.POST);

        //request.AddJsonBody(new ExtchangeTokenRequestBody()
        //{
        //    AccessToken = this.currentContextToken,
        //    RefreshToken = this.refereshToken
        //});

        //var response = client.Execute(request);

        //this.currentContextToken = new JsonDeserializer().Deserialize<LoginResponse>(response)?.AccessToken;


        //   }

        [Test, Order(2)]
        public void GetReactantList()
        {

            if (string.IsNullOrEmpty(this.currentContextToken))
                throw new Exception("Access Token Not Valid");

            var client = new RestClient(APIRequestConstants.APIBaseURL);
            //var request = new RestRequest("api/reactant-lists?page={PageNumber}&pageSize={PageSize}&ownedReactantListsOnly={OwnedRLOnly}&SortMetadata[0].Field={SortMetaData}&SortMetadata[0].Direction={SortMetaDataDirection}&isActive={ActiveFlage}", Method.GET);
            var request = new RestRequest(APIRequestConstants.ReactantListURL, Method.GET);

            request.AddQueryParameter("page", "1");
            request.AddQueryParameter("pageSize", "10");
            request.AddQueryParameter("ownedReactantListsOnly", "true");
            request.AddQueryParameter("SortMetadata[0].Field", "updatedAt");
            request.AddQueryParameter("SortMetadata[0].Direction", "-1");
            request.AddQueryParameter("isActive", "true");


            request.AddHeader(APIRequestConstants.AuthorizationHeaderKey, $"Bearer {this.currentContextToken}");

            //            var content = client.Execute(request).StatusCode;
            var response = client.Execute(request);

            var rl = new JsonDeserializer().Deserialize<ReactantListResponse>(response);
            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(rl.results.Count <= 10);
        }

        [Test, Order(1)]
        public void ReactantListAdd()
        {   //TC1
            //arrange

            
            var client = new RestClient(APIRequestConstants.APIBaseURL);
            var request = new RestRequest(APIRequestConstants.ReactantListURL, Method.POST);

            ReactantListRequestBody reactantListRequestBody = new ReactantListRequestBody()
            {
                Name = "APITest-RL-" + random.Next().ToString(),
                Description = "",
                ReactantIds = new List<int>() { 1, 3 }
            };
            request.AddJsonBody(reactantListRequestBody);//.Body = new RequestBody("application/json","", reactantListRequestBody);

            request.AddHeader(APIRequestConstants.AuthorizationHeaderKey, $"Bearer {this.currentContextToken}");



            //Act
            var response = client.Execute(request);
            createdReactantListID =int.Parse(response.Content);
            var statusCode = response.StatusCode;

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, statusCode);
        }


        [Test, Order(3)]
        public void ReactantListEdit()
        {   
            //arrange
            var client = new RestClient(APIRequestConstants.APIBaseURL);
            var request = new RestRequest(APIRequestConstants.ReacntaListURLwithID, Method.PUT);

            request.AddUrlSegment(APIRequestConstants.URLSegmaentID, createdReactantListID);
            ReactantListRequestBody reactantListRequestBody = new ReactantListRequestBody()
            {
                Name = "APITest-RL-" + random.Next().ToString(),
                Description = "",
                ReactantIds = new List<int>() { 1, 3 }
            };
            request.AddJsonBody(reactantListRequestBody);//.Body = new RequestBody("application/json","", reactantListRequestBody);

            request.AddHeader(APIRequestConstants.AuthorizationHeaderKey, $"Bearer {this.currentContextToken}");

            //Act
            var statusCode = client.Execute(request).StatusCode;
            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, statusCode);
        }


        [Test, Order(4)]
      
        public void ReactantListDelete()
        {
            //arrange
            var client = new RestClient(APIRequestConstants.APIBaseURL);
            var request = new RestRequest(APIRequestConstants.ReacntaListURLwithID, Method.DELETE);

            request.AddUrlSegment(APIRequestConstants.URLSegmaentID, createdReactantListID);
            request.AddHeader(APIRequestConstants.AuthorizationHeaderKey, $"Bearer {this.currentContextToken}");

            //Act
            var statusCode = client.Execute(request).StatusCode;
            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, statusCode);
        }

        public void TearDown()
        {

        }
    }
}
