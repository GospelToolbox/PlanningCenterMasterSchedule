using System;
using System.Collections.Generic;

namespace PlanningCenterApi
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public Dictionary<string, string> Links { get; set; }

        public List<GenericData> Included { get; set; }
    }
}
