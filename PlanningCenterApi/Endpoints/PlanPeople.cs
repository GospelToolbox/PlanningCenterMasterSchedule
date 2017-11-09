using PlanningCenterApi.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace PlanningCenterApi.Endpoints
{
    public class PlanPeople : GenericCollectionEndpoint<PlanPerson>
    {
        internal PlanPeople(IRestClient client, string endpointPath) : base(client, endpointPath)
        {
        }

        public PlanPerson[] List(string[] includes = null)
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

            return Get<PlanPerson[]>(queryString).Data;
        }
    }
}
