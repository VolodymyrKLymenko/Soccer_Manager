using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Soccer_Manager.Models;

using Soccer_Manager.ModelClasses;

namespace Soccer_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataProvicer dataProvider;

        public HomeController(DataProvicer provider)
        {
            dataProvider = provider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SoccerManager()
        {
            Team t = dataProvider.teams.First();

            return View("SoccerManager", dataProvider.teams);
        }

        //public IActionResult SoccerManager()
        //{
        //    return View(highLevelService.GetAllTeam());
        //}
    }
}
