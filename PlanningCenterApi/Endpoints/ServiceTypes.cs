using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using PlanningCenterApi.Contracts;

namespace PlanningCenterApi.Endpoints
{
    public class ServiceTypes : GenericCollectionEndpoint<ServiceType>
    {
        internal ServiceTypes(IRestClient client, string baseUrl) : base(client, baseUrl)
        {
        }

        public Plans Plans(string serviceTypeId)
        {
            return new Plans(Client, $"{EndpointPath}/{serviceTypeId}/plans");
        }
            
        public GenericCollectionEndpoint<Attachment> Attachments(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<Attachment>(Client, $"{EndpointPath}/{serviceTypeId}/attachments");
        }

        public GenericCollectionEndpoint<ItemNoteCategory> ItemNoteCategories(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<ItemNoteCategory>(Client, $"{EndpointPath}/{serviceTypeId}/item_note_categories");
        }

        public GenericCollectionEndpoint<Layout> Layouts(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<Layout>(Client, $"{EndpointPath}/{serviceTypeId}/layouts");
        }

        public GenericCollectionEndpoint<PlanNoteCategory> PlanNoteCategories(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<PlanNoteCategory>(Client, $"{EndpointPath}/{serviceTypeId}/plan_note_categories");
        }

        public GenericCollectionEndpoint<Schedule> PlanTemplates(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<Schedule>(Client, $"{EndpointPath}/{serviceTypeId}/plan_templates");
        }

        public GenericCollectionEndpoint<TeamPosition> TeamPositions(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<TeamPosition>(Client, $"{EndpointPath}/{serviceTypeId}/team_positions");
        }

        public GenericCollectionEndpoint<Team> Teams(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<Team>(Client, $"{EndpointPath}/{serviceTypeId}/teams");
        }

        public GenericCollectionEndpoint<TimePreferenceOption> TimePreferenceOptions(string serviceTypeId)
        {
            return new GenericCollectionEndpoint<TimePreferenceOption>(Client, $"{EndpointPath}/{serviceTypeId}/time_preference_options");
        }
    }
}
