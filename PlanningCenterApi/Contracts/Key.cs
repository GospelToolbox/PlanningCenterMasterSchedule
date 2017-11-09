using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Key: BaseContract
    {
        public AlternateKey[] AlternateKeys { get; set; }
      
        public string EndingKey { get; set; }

        public bool EndingMinor { get; set; }

        public string Name { get; set; }

        public string StartingKey { get; set; }

        public bool StartingMinor { get; set; }
    }
}
