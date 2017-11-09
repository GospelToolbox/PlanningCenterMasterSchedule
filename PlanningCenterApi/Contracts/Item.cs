using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Item: BaseContract
    {
        public string Description { get; set; }

        public string HtmlDetails { get; set; }

        public string ItemType { get; set; }

        public string KeyName { get; set; }

        public int Length { get; set; }

        public int Sequence { get; set; }

        public string ServicePosition { get; set; }

        public string Title { get; set; }

        public Plan Plan { get; set; }

        public Song Song { get; set; }

        public Arrangement Arrangement { get; set; }

        public Key Key { get; set; }

        public Layout SelectedLayout { get; set; }
    }
}
