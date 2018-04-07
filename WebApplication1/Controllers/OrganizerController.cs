using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Model_Classes;

namespace WebApplication1.Controllers
{
    public class OrganizerController : Controller
    {
        private SoccerContext provider;

        public OrganizerController(SoccerContext dataProvider)
        {
            provider = dataProvider;
        }

        public IActionResult Index(Tournament tournament = null)
        {

            return View(provider.Tournaments.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tournament tournament)
        {
            provider.Tournaments.Add(tournament);
            provider.SaveChanges();

            return RedirectToAction("Index", tournament);
        }
    }
}