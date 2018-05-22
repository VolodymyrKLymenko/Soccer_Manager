using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Model_Classes;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using Services;
using WebApplication1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Team")]
    public class TeamController : Controller
    {
        private IHighLevelSoccerManagerService highProvider;
        private ILowLevelSoccerManagmentService lowProvider;
        private readonly UserManager<DAL.Model_Classes.User> _userManager;

        private const string TeamKey = "team";

        public TeamController(IHighLevelSoccerManagerService high, ILowLevelSoccerManagmentService low, UserManager<DAL.Model_Classes.User> userManager)
        {
            highProvider = high;
            lowProvider = low;
            _userManager = userManager;
        }

        private Task<DAL.Model_Classes.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private async Task<Team> CurrentTeam()
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            return highProvider.GetAllTeam().FirstOrDefault(t => t.TeamId == tournament_id);
        }

        public async Task<IActionResult> Index()
        {
            Team team = await CurrentTeam();

            return View(new TeamMainInfo()
            {
                Team = team,
                Cups = highProvider.GetAllTournaments().ToList()
            });
        }

        [HttpGet]
        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(Player player)
        {
            Team team = await CurrentTeam();

            if (ModelState.IsValid)
            {
                lowProvider.CreatePlayerForTeam(team.TeamId, player);
                highProvider.UpdateTeam(team.TeamId, team);
                TempData["message"] = $"{player.Name} has been added";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(player);
            }
        }


        [HttpGet]
        public IActionResult AddReward()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReward(Reward reward)
        {
            Team team = await CurrentTeam();

            if (ModelState.IsValid)
            {
                lowProvider.AddRewardForTeam(team.TeamId, reward);
                highProvider.UpdateTeam(team.TeamId, team);
                TempData["message"] = $"{reward.Name} has been added";
                return RedirectToAction("ListReward");
            }

            else
            {
                return View(reward);
            }
        }

        [HttpGet]
        public IActionResult EditReward(int rewardId)
        {
            Reward reward = lowProvider.GetReward(rewardId);

            return View(reward);
        }

        [HttpPost]
        public IActionResult EditReward(Reward reward)
        {
            if (ModelState.IsValid)
            {
                lowProvider.UpdateReward(reward.RewardId, reward);
                TempData["message"] = $"{reward.Name} has been saved";
                return RedirectToAction("ListReward");
            }
            else
            {
                // there is something wrong with the data values
                return View(reward);
            }
        }

        public async Task<IActionResult> RemoveReward(int RewardId, string Password)
        {
            Team team = await CurrentTeam();

            TempData["message"] = $"{lowProvider.GetReward(RewardId).Name} was removed";
            lowProvider.RemoveReward(RewardId);

            return RedirectToAction("ListReward");
        }

        public async Task<IActionResult> ListReward()
        {
            Team team = await CurrentTeam();

            if (team != null)
            {
                return View(lowProvider.GetTeamRewards(team.TeamId));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult EditPlayer(int playerId)
        {
            Player player = lowProvider.GetPlayer(playerId);

            return View(player);
        }

        [HttpPost]
        public IActionResult EditPlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                lowProvider.UpdatePlayer(player.PlayerId, player);
                TempData["message"] = $"{player.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(player);
            }
        }

        public async Task<IActionResult> RemovePlayer(int PlayerId, string Password)
        {
            Team team = await CurrentTeam();

            if (Password == team.Password)
            {
                TempData["message"] = $"{lowProvider.GetPlayer(PlayerId).Name} was removed";
                lowProvider.RemovePlayer(PlayerId);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            Team team = await CurrentTeam();

            return View(team);
        }

        [HttpPost]
        public async Task<ViewResult> Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                highProvider.UpdateTeam(team.TeamId, team);
                Team _team = await CurrentTeam();

                TempData["message"] = $"{_team.Name} has been saved";

                return View("Index", new TeamMainInfo()
                {
                    Team = _team,
                    ShowConfirming = false,
                    Cups = highProvider.GetAllTournaments().ToList()
                });
            }
            else
            {
                return View(team);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            Team team = await CurrentTeam();

            return View("Index", new TeamMainInfo()
            {
                Team = team,
                ShowConfirming = true,
                Cups = highProvider.GetAllTournaments().ToList()
            });
        }

        public async Task<IActionResult> Delete()
        {
            Team team = await CurrentTeam();

            foreach (var i in team.Players)
            {
                lowProvider.RemovePlayer(i.PlayerId);
            }
            highProvider.RemoveTeam(team.TeamId);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveCup(int CupId)
        {
            Team team = await CurrentTeam();

                TempData["message"] = $"{highProvider.GetTournament(CupId)?.Name} was removed";
                highProvider.RemoveTeamFromTournament(team.TeamId, CupId);

            return RedirectToAction("Index", "Team");
        }

        public async Task<IActionResult> RegistrToCup(int CupId, string Password)
        {
            Team team = await CurrentTeam();

            if (Password == team.Password)
            {
                TempData["message"] = $"You have been registr for the {highProvider.GetTournament(CupId).Name}";
                highProvider.AddTeamToTournament(team.TeamId, CupId);
            }

            return RedirectToAction("Index", "Team");
        }
    }
}