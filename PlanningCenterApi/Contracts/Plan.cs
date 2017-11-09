using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Plan: BaseContract
    {
        public string Dates { get; set; }

        public DateTimeOffset? FilesExpireAt { get; set; }

        public int ItemsCount { get; set; }

        public DateTimeOffset? LastTimeAt { get; set; }

        public bool MultiDay { get; set; }

        public int NeededPositionsCount { get; set; }

        public int OtherTimeCount { get; set; } 

        public string Permissions { get; set; }

        public int PlanNotesCount { get; set; }

        public int PlanPeopleCount { get; set; }

        public bool Public { get; set; }

        public int RehearsalTimeCount { get; set; }

        public string SeriesTitle { get; set; }

        public int ServiceTimeCount { get; set; }

        public string ShortDates { get; set; }

        public DateTimeOffset SortDate { get; set; }

        public string Title { get; set; }

        public int TotalLength { get; set; }
    }
}
