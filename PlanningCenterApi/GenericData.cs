using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi
{
    public class GenericData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Dictionary<string, object> Attributes { get; set; }

        public Dictionary<string, dynamic> Relationships { get; set; }
    }
}
