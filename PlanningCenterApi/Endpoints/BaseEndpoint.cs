using JsonApiSerializer;
using JsonApiSerializer.ContractResolvers;
using JsonApiSerializer.JsonApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Endpoints
{
    public abstract class BaseEndpoint
    {
        protected IRestClient Client { get; }
        protected JsonSerializerSettings JsonSettings { get; }
        protected string EndpointPath { get; }

        public BaseEndpoint(string clientId, string clientSecret, string baseUrl)
        {
            
            Client = new RestClient(baseUrl);
            Client.Authenticator = new HttpBasicAuthenticator(clientId, clientSecret);

            JsonSettings = new JsonApiSerializerSettings()
            {
                ContractResolver = new JsonApiContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(true, false)
                }
            };
        }

        internal BaseEndpoint(IRestClient client, string endpointPath)
        {
            EndpointPath = endpointPath;
            this.Client = client;

            JsonSettings = new JsonApiSerializerSettings()
            {
                ContractResolver = new JsonApiContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(true, false)
                }
            };
        }

        protected DocumentRoot<T> Get<T>(string path = null)
        {
            var request = new RestRequest($"{EndpointPath}{path}", Method.GET);
            var response = Client.Execute(request);
            var json = response.Content;
            return JsonConvert.DeserializeObject<DocumentRoot<T>>(json, JsonSettings);
        }
    }
}
