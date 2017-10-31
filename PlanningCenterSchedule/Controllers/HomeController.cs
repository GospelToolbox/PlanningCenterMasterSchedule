using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PlanningCenterSchedule.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [AccessList]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Request()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
