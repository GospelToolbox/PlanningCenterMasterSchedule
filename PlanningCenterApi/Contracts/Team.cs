using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Team: BaseContract
    {
        public bool AssignedDirectly { get; set; }

        public bool DefaultPrepareNotifications { get; set; }

        public string DefaultStatus { get; set; }

        public string Name { get; set; }

        public string ScheduleTo { get; set; }

        public int? Sequence { get; set; }
    }
}
