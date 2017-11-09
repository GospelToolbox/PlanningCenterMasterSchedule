using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Song: BaseContract
    {
        public string Admin { get; set; }

        public string Author { get; set; }

        [JsonProperty(propertyName: "ccli_number")]
        public int CCLINumber { get; set; }

        public string Copyright { get; set; }

        public bool Hidden { get; set; }

        public string Themes { get; set; }

        public string Title { get; set; }
    }
}
