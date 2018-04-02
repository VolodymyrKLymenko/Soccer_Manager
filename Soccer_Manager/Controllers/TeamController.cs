using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ModelClasses;
using Services;

namespace Soccer_Manager.Controllers
{
    public class TeamController : Controller
    {
        /*private readonly IHighLevelSoccerManagerService highLevelSoccerManagerService;
        private readonly ILowLevelSoccerManagmentService lowLevelSoccerManagmentService;


        public TeamController(IHighLevelSoccerManagerService highService,
            ILowLevelSoccerManagmentService lowService)
        {
            highLevelSoccerManagerService = highService;
            lowLevelSoccerManagmentService = lowService;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SoccerManager()
        {
            return View();
        }
    }
}