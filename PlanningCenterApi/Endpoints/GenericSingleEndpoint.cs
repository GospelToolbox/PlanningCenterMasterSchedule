using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace PlanningCenterApi.Endpoints
{
    public class GenericSingleEndpoint<T> : BaseEndpoint
    {
        internal GenericSingleEndpoint(IRestClient client, string endpointPath) : base(client, endpointPath)
        {
        }

        public T Get(string id)
        {
            return Get<T>().Data;
        }
    }
}
