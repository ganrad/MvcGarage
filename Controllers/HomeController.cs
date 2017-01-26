using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcGarage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "My Garage Application";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Red Hat Inc.,";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
