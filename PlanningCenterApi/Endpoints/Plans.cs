using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using PlanningCenterApi.Contracts;

namespace PlanningCenterApi.Endpoints
{
    public class Plans : GenericCollectionEndpoint<Plan>
    {
        internal Plans(IRestClient client, string baseUrl) : base(client, baseUrl)
        {
        }

        public new Plan[] List(bool future = true)
        {
            var queryString = "";
            var queryParams = new List<string>();

            if(future)
            {
                queryParams.Add("filter=future");
            }

            if(queryParams.Count > 0)
            {
                queryString = $"?{string.Join("&", queryParams)}";
            }

            return Get<Plan[]>(queryString).Data;
        }

        public GenericCollectionEndpoint<Attachment> AllAttachments(string planId)
        {
            return new GenericCollectionEndpoint<Attachment>(Client, $"{EndpointPath}/{planId}/all_attachments");
        }

        public GenericCollectionEndpoint<Attachment> Attachments(string planId)
        {
            return new GenericCollectionEndpoint<Attachment>(Client, $"{EndpointPath}/{planId}/attachments");
        }

        public GenericCollectionEndpoint<Contributor> Contributors(string planId)
        {
            return new GenericCollectionEndpoint<Contributor>(Client, $"{EndpointPath}/{planId}/contributors");
        }

        public Items Items(string planId)
        {
            return new Items(Client, $"{EndpointPath}/{planId}/items");
        }

        public GenericCollectionEndpoint<Schedule> MySchedules(string planId)
        {
            return new GenericCollectionEndpoint<Schedule>(Client, $"{EndpointPath}/{planId}/my_schedules");
        }

        public GenericCollectionEndpoint<NeededPosition> NeededPositions(string planId)
        {
            return new GenericCollectionEndpoint<NeededPosition>(Client, $"{EndpointPath}/{planId}/needed_positions");
        }

        public GenericSingleEndpoint<Plan> NextPlan(string planId)
        {
            return new GenericSingleEndpoint<Plan>(Client, $"{EndpointPath}/{planId}/next_plan");
        }

        public GenericCollectionEndpoint<PlanNote> Notes(string planId)
        {
            return new GenericCollectionEndpoint<PlanNote>(Client, $"{EndpointPath}/{planId}/notes");
        }

        public GenericCollectionEndpoint<PlanTime> PlanTimes(string planId)
        {
            return new GenericCollectionEndpoint<PlanTime>(Client, $"{EndpointPath}/{planId}/plan_times");
        }

        public GenericSingleEndpoint<Plan> PreviousPlan(string planId)
        {
            return new GenericSingleEndpoint<Plan>(Client, $"{EndpointPath}/{planId}/previous_plan");
        }

        public GenericSingleEndpoint<Series> Series(string planId)
        {
            return new GenericSingleEndpoint<Series>(Client, $"{EndpointPath}/{planId}/series");
        }

        public GenericCollectionEndpoint<Team> SignupTeams(string planId)
        {
            return new GenericCollectionEndpoint<Team>(Client, $"{EndpointPath}/{planId}/signup_teams");
        }

        public PlanPeople TeamMembers(string planId)
        {

            return new PlanPeople(Client, $"{EndpointPath}/{planId}/team_members");
        }
    }
}
