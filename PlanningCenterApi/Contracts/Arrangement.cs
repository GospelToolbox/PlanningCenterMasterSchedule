using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Arrangement: BaseContract
    {
        public DateTimeOffset? ArchivedAt { get; set; }

        [JsonProperty(propertyName: "bpm")]
        public decimal? BPM { get; set; }

        public string ChordChart { get; set; }

        public int ChordChartChordColor { get; set; }

        public int ChordChartColumns { get; set; }

        public string ChordChartFont { get; set; }

        public int ChordChartFontSize { get; set; }

        public string ChordChartKey { get; set; }

        public bool HasChordChart { get; set; }

        public bool HasChords { get; set; }

        public int Length { get; set; }

        public string Meter { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string[] Sequence { get; set; }
    }
}
