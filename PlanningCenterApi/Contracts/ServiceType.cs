using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class ServiceType: BaseContract
    {
        public bool AttachmentTypesEnabled { get; set; }

        public string Frequency { get; set; }

        public string Name { get; set; }

        public string Permissions { get; set; }

        public int Sequence { get; set; }
    }
}
