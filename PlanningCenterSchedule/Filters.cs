using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningCenterSchedule
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AccessListAttribute : TypeFilterAttribute
    {
        public AccessListAttribute() : base(typeof(AccessListFilter))
        {
        }
    }

    public class AccessListFilter : IAuthorizationFilter
    {
        public AccessListFilter()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var email = context.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var allowedEmailAddresses = File.ReadAllLines("./Data/AccessList.txt")
                .Where(entry => !entry.StartsWith("#"))
                .Select(entry => entry.ToLower().Trim());
            if (email == null || !allowedEmailAddresses.Contains(email.ToLower()))
            {
                context.Result = new RedirectResult("/home/request");
            }
        }
    }
}
