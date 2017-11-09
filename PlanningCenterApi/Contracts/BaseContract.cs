using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class BaseContract
    {
        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
