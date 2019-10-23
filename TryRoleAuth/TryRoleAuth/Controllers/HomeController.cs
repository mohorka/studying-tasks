using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TryRoleAuth.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TryRoleAuth.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="admin,user")]

        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

            return Content($"ваша роль: {role}");

        }
        [Authorize(Roles="admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your app description page.";
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
