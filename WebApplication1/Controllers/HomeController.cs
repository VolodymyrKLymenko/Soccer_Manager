using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ViewModels;
using DAL;
using DAL.Model_Classes;
using WebApplication1.Models;
using Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IHighLevelSoccerManagerService highService;
        private ILowLevelSoccerManagmentService  lowService;

        public HomeController(IHighLevelSoccerManagerService high,
            ILowLevelSoccerManagmentService low)
        {
            highService = high;
            lowService  = low;
        }

        public IActionResult Index()
        {
            GeneralInfo general = new GeneralInfo();
            general.Players = lowService.GetAllPlayers().ToList();
            general.Teams = highService.GetAllTeam().ToList();
            general.Tournaments = highService.GetAllTournaments().ToList();

            general.RecalculateAge();

            return View(general);
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

        [HttpGet]
        public IActionResult Teams(string searchString)
        {
            var teams = highService.GetAllTeam().ToList();

            List<Team> _teams = new List<Team>();

            if (!String.IsNullOrEmpty(searchString))
            {
                foreach(var r in lowService.GetAllRewards())
                {
                    if (r.Name.Contains(searchString))
                    {
                        _teams.Add(highService.GetTeam(r.Team.TeamId));
                        break;
                    }
                }
            }

            else
            {
                _teams = teams;
            }

            return View(_teams);
        }

        public IActionResult Cup(int id)
        {
            Tournament t = highService.GetTournament(id);

            return View("Cup", t);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
