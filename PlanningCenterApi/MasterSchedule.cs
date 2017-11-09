using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi
{
    public class MasterSchedule
    {
        /// <summary>
        /// Mapping of service type Id to Name
        /// </summary>
        public Dictionary<string, string> ServiceTypes { get; set; } = new Dictionary<string, string>();

        public SortedSet<string> ScheduleDates { get; set; } = new SortedSet<string>();

        public Dictionary<string, HashSet<string>> ServiceTypeTeams { get; set; } = new Dictionary<string, HashSet<string>>();

        public Dictionary<string, string> TeamNames { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, HashSet<string>> TeamPositions { get; set; } = new Dictionary<string, HashSet<string>>();

        // ServiceType -> Team -> Role -> Date
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>>> Records { get; set; } = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, HashSet<ScheduledPosition>>>>>();

        
    }
}
