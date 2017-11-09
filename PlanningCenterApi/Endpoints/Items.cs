using PlanningCenterApi.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace PlanningCenterApi.Endpoints
{
    public class Items : GenericCollectionEndpoint<PlanPerson>
    {
        internal Items(IRestClient client, string endpointPath) : base(client, endpointPath)
        {
        }

        public Item[] List(string[] includes = null)
        {
            var queryString = "";
            var queryParams = new List<string>();

            if (includes != null && includes.Length > 0)
            {
                queryParams.Add($"include={string.Join(",", includes)}");
            }

            if (queryParams.Count > 0)
            {
                queryString = $"?{string.Join("&", queryParams)}";
            }

            return Get<Item[]>(queryString).Data;
        }
    }
}
