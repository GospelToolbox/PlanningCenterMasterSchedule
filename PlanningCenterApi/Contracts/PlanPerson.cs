using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class PlanPerson : BaseContract
    {
        public bool CanAcceptPartial { get; set; }

        public string DeclineReason { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset? NotificationSentAt { get; set; }

        public string PhotoThumbnail { get; set; }

        public bool PrepareNotification { get; set; }

        public string Status { get; set; }

        public DateTimeOffset? StatusUpdatedAt { get; set; }

        public string TeamPositionName { get; set; }

        public Person Person { get; set; }

        public Plan Plan { get; set; }

        public ServiceType ServiceType { get; set; }

        public Team Team { get; set; }

        public Person RespondsTo { get; set; }

        public PlanTime[] Times { get; set; }
    }
}
