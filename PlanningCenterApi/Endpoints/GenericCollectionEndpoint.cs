using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace PlanningCenterApi.Endpoints
{
    public class GenericCollectionEndpoint<T> : BaseEndpoint
    {
        internal GenericCollectionEndpoint(IRestClient client, string endpointPath) : base(client, endpointPath)
        {
        }

        public T[] List()
        {
            return Get<T[]>().Data;
        }

        public T Get(string id)
        {
            return Get<T>($"{id}").Data;
        }
    }
}
