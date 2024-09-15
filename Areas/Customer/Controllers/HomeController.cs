using Banha_UniverCity.Areas.Admin.Controllers;
using Banha_UniverCity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Banha_UniverCity.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(StaticData.role_Admin))
            {
                return RedirectToAction("Index", "DashBoard", new {Area=StaticData.role_Admin});
            }
            if (User.IsInRole(StaticData.role_Instructor))
            {
                return RedirectToAction("Dashboard", "Instructor", new { Area = StaticData.role_Instructor });
            }
            if (User.IsInRole(StaticData.role_Student))
            {
                return RedirectToAction("Index", "DashBoard", new { Area = StaticData.role_Student });

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
