using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningCenterApi.Contracts
{
    public class Person: BaseContract
    {
        public DateTimeOffset? Anniversary { get; set; }

        public DateTimeOffset? Birthdate { get; set; }

        public int? FacebookId { get; set; }

        public string FirstName { get; set; }

        public string IcalCode { get; set; }

        public string LastName { get; set; }

        public string LegacyId { get; set; }

        public DateTimeOffset? LoggedInAt { get; set; }

        public string MaxPermissions { get; set; }

        public string MeTab { get; set; }

        public string MediaTab { get; set; }

        public string MiddleName { get; set; }
        
        public string Notes { get; set; }

        public bool PassedBackgroundCheck { get; set; }

        public string PeopleTab { get; set; }

        public string Permissions { get; set; }

        public string PhotoThumbnailUrl { get; set; }

        public string PhotoUrl { get; set; }

        public string PlansTab { get; set; }

        public bool SiteAdministrator { get; set; }

        public string SongsTab { get; set; }

        public string Status { get; set; }
    }
}
