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
        public MasterSchedule GetSchedule()
        {
            var dataJson = System.IO.File.ReadAllText("./Data/data.json");
            return JsonConvert.DeserializeObject<MasterSchedule>(dataJson);
        }

        [HttpPost("[action]")]
        public void RefreshSchedule()
        {
            lock (sync)
            {

                var pco = new PlanningCenter(Configuration["PlanningCenter:ClientId"], Configuration["PlanningCenter:ClientSecret"]);

                var serviceTypes = pco.Services.ServiceTypes().List();

                var schedule = new MasterSchedule();

                foreach (var serviceType in serviceTypes)
                {
                    var plans = pco.Services.ServiceTypes().Plans(serviceType.Id).List(future: true);

                    schedule.ServiceTypes[serviceType.Id] = serviceType.Name;
                    schedule.ServiceTypeTeams[serviceType.Id] = new HashSet<string>();
                    schedule.Records[serviceType.Id] = new Dictionary<string, Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>>();

                    foreach (var plan in plans)
                    {
                        var planDateString = plan.LastTimeAt?.ToString("yyyy-MM-dd");
                        schedule.ScheduleDates.Add(planDateString);

                        var planPeople = pco.Services.ServiceTypes().Plans(serviceType.Id).TeamMembers(plan.Id).List(includes: new[] { "team", "person" });

                        foreach (var planPerson in planPeople)
                        {
                            var team = planPerson.Team;

                            if (!schedule.Records[serviceType.Id].ContainsKey(team.Id))
                            {
                                schedule.Records[serviceType.Id][team.Id] = new Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>();
                            }

                            schedule.TeamNames[team.Id] = team.Name;
                            schedule.ServiceTypeTeams[serviceType.Id].Add(team.Id);

                            if (!schedule.TeamPositions.ContainsKey(team.Id))
                            {
                                schedule.TeamPositions[team.Id] = new HashSet<string>();
                            }

                            var position = planPerson.TeamPositionName ?? "(other)";

                            if (!schedule.Records[serviceType.Id][team.Id].ContainsKey(position))
                            {
                                schedule.Records[serviceType.Id][team.Id][position] = new Dictionary<string, HashSet<ScheduledPosition>>();
                            }

                            schedule.TeamPositions[team.Id].Add(position);

                            if (!schedule.Records[serviceType.Id][team.Id][position].ContainsKey(planDateString))
                            {
                                schedule.Records[serviceType.Id][team.Id][position][planDateString] = new HashSet<ScheduledPosition>();
                            }

                            schedule.Records[serviceType.Id][team.Id][position][planDateString].Add(new ScheduledPosition
                            {
                                Name = planPerson.Name,
                                Status = planPerson.Status,
                                NotificationSent = planPerson.NotificationSentAt.HasValue
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
