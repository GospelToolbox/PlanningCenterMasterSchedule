using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningCenterApi;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Configuration;

namespace PlanningCenterSchedule.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [AccessList]
    public class ScheduleController : Controller
    {
        private static object sync = new object();

        public ScheduleController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        [HttpGet("[action]")]
        public Schedule GetSchedule()
        {
            var dataJson = System.IO.File.ReadAllText("./Data/data.json");
            return JsonConvert.DeserializeObject<Schedule>(dataJson);
        }

        [HttpPost("[action]")]
        public void RefreshSchedule()
        {
            lock (sync)
            {
                var client = new RestClient("https://api.planningcenteronline.com");
                client.Authenticator = new HttpBasicAuthenticator(
                    Configuration["PlanningCenter:ClientId"],
                    Configuration["PlanningCenter:ClientSecret"]);

                var request = new RestRequest("services/v2/service_types", Method.GET);

                var response = client.Execute<ApiResponse<List<GenericData>>>(request);

                var schedule = new Schedule();

                foreach (var serviceType in response.Data.Data)
                {
                    var plansRequest = new RestRequest($"services/v2/service_types/{serviceType.Id}/plans");
                    plansRequest.AddQueryParameter("filter", "future");

                    var plansResponse = client.Execute<ApiResponse<List<GenericData>>>(plansRequest);

                    schedule.ServiceTypes[serviceType.Id] = serviceType.Attributes["name"] as string;
                    schedule.ServiceTypeTeams[serviceType.Id] = new HashSet<string>();
                    schedule.Records[serviceType.Id] = new Dictionary<string, Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>>();

                    foreach (var plan in plansResponse.Data.Data)
                    {
                        var planDate = DateTimeOffset.Parse(plan.Attributes["last_time_at"] as string);
                        var planDateString = planDate.ToString("yyyy-MM-dd");
                        schedule.ScheduleDates.Add(planDateString);

                        var peopleRequest = new RestRequest($"/services/v2/service_types/{serviceType.Id}/plans/{plan.Id}/team_members", Method.GET);
                        peopleRequest.AddQueryParameter("include", "team,person");

                        var peopleResponse = client.Execute<ApiResponse<List<GenericData>>>(peopleRequest);

                        var teams = peopleResponse.Data.Included.Where(d => d.Type == "Team").ToDictionary(d => d.Id, d => d.Attributes["name"] as string);

                        foreach (var planPerson in peopleResponse.Data.Data)
                        {
                            var teamId = planPerson.Relationships["team"]["data"]["id"] as string;
                            if (!schedule.Records[serviceType.Id].ContainsKey(teamId))
                            {
                                schedule.Records[serviceType.Id][teamId] = new Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>();
                            }

                            schedule.TeamNames[teamId] = teams[teamId];
                            schedule.ServiceTypeTeams[serviceType.Id].Add(teamId);

                            if (!schedule.TeamPositions.ContainsKey(teamId))
                            {
                                schedule.TeamPositions[teamId] = new HashSet<string>();
                            }

                            var position = planPerson.Attributes["team_position_name"] as string ?? "(other)";

                            if (!schedule.Records[serviceType.Id][teamId].ContainsKey(position))
                            {
                                schedule.Records[serviceType.Id][teamId][position] = new Dictionary<string, HashSet<ScheduledPosition>>();
                            }

                            schedule.TeamPositions[teamId].Add(position);

                            if (!schedule.Records[serviceType.Id][teamId][position].ContainsKey(planDateString))
                            {
                                schedule.Records[serviceType.Id][teamId][position][planDateString] = new HashSet<ScheduledPosition>();
                            }

                            schedule.Records[serviceType.Id][teamId][position][planDateString].Add(new ScheduledPosition
                            {
                                Name = planPerson.Attributes["name"] as string,
                                Status = planPerson.Attributes["status"] as string,
                                NotificationSent = planPerson.Attributes["notification_sent_at"] != null
                            });
                        }
                    }
                }

                var scheduleJson = JsonConvert.SerializeObject(schedule);
                System.IO.File.WriteAllText("./Data/data.json", scheduleJson);
            }
        }
    }
}
