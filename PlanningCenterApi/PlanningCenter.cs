using PlanningCenterApi.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi
{
    public class PlanningCenter
    {
        public const string BASE_URL = "https://api.planningcenteronline.com";

        public Services Services { get; }

        public PlanningCenter(string clientId, string clientSecret, string baseUrl = BASE_URL)
        {
            Services = new Services(clientId, clientSecret, $"{baseUrl}/services/v2");
        }
    }
}
