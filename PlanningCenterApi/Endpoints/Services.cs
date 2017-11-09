using JsonApiSerializer;
using JsonApiSerializer.JsonApi;
using Newtonsoft.Json;
using PlanningCenterApi.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Endpoints
{
    public class Services: BaseEndpoint
    {
        public Services(string clientId, string clientSecret, string baseUrl) : base(clientId, clientSecret, baseUrl)
        {
        }

        public ServiceTypes ServiceTypes()
        {
            return new ServiceTypes(Client, $"{EndpointPath}/service_types");
        }
    }
}
