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
        private readonly IHighLevelSoccerManagerService _highProvider;
        private readonly ILowLevelSoccerManagmentService _lowProvider;
        private readonly UserManager<DAL.Model_Classes.User> _userManager;

        private const string TeamKey = "team";

        public TeamController(IHighLevelSoccerManagerService high, ILowLevelSoccerManagmentService low, UserManager<DAL.Model_Classes.User> userManager)
        {
            _highProvider = high;
            _lowProvider = low;
            _userManager = userManager;
        }

        private Task<DAL.Model_Classes.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private async Task<Team> CurrentTeam()
        {
            int tournament_id = (await GetCurrentUserAsync()).UserId;
            return _highProvider.GetAllTeam().FirstOrDefault(t => t.TeamId == tournament_id);
        }

        public async Task<IActionResult> Index()
        {
            Team team = await CurrentTeam();
            LowLevelSoccerManagerService.RecalculateAge(team.Players);

            return View(new TeamMainInfo()
            {
                Team = team,
                Cups = _highProvider.GetAllTournaments().ToList(),
                RegisteredCups = _highProvider.GetAllTournaments()
                    .Where(el => el.TeamTournaments.Any(tt => tt.TournamentId == team.TeamId)).ToList()
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
            
            _lowProvider.CreatePlayerForTeam(team.TeamId, player);
            _highProvider.UpdateTeam(team.TeamId, team);
            TempData["message"] = $"{player.Name} has been added";

            return RedirectToAction("Index");
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

            _lowProvider.AddRewardForTeam(team.TeamId, reward);
            _highProvider.UpdateTeam(team.TeamId, team);
            TempData["message"] = $"{reward.Name} has been added";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditReward(int rewardId)
        {
            Reward reward = _lowProvider.GetReward(rewardId);

            return View(reward);
        }

        [HttpPost]
        public IActionResult EditReward(Reward reward)
        {
            _lowProvider.UpdateReward(reward.RewardId, reward);
            TempData["message"] = $"{reward.Name} has been saved";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveReward(int rewardId)
        {
            Team team = await CurrentTeam();

            TempData["message"] = $"{_lowProvider.GetReward(rewardId).Name} was removed";
            _lowProvider.RemoveReward(rewardId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPlayer(int playerId)
        {
            Player player = _lowProvider.GetPlayer(playerId);

            return View(player);
        }

        [HttpPost]
        public IActionResult EditPlayer(Player player)
        {
            _lowProvider.UpdatePlayer(player.PlayerId, player);
            TempData["message"] = $"{player.Name} has been saved";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemovePlayer(int playerId)
        {
            Team team = await CurrentTeam();

            TempData["message"] = $"{_lowProvider.GetPlayer(playerId).Name} was removed";
            _lowProvider.RemovePlayer(playerId);

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
            _highProvider.UpdateTeam(team.TeamId, team);
            Team _team = await CurrentTeam();

            TempData["message"] = $"{_team.Name} has been saved";

            return View("Index", new TeamMainInfo()
            {
                Team = _team,
                ShowConfirming = false,
                Cups = _highProvider.GetAllTournaments().ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            Team team = await CurrentTeam();

            return View("Index", new TeamMainInfo()
            {
                Team = team,
                ShowConfirming = true,
                Cups = _highProvider.GetAllTournaments().ToList()
            });
        }

        public async Task<IActionResult> Delete()
        {
            Team team = await CurrentTeam();

            foreach (var i in team.Players)
            {
                _lowProvider.RemovePlayer(i.PlayerId);
            }
            _highProvider.RemoveTeam(team.TeamId);
            var user = await GetCurrentUserAsync();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Logout", "Account");
        }

        public async Task<IActionResult> RemoveCup(int CupId)
        {
            Team team = await CurrentTeam();

                TempData["message"] = $"{_highProvider.GetTournament(CupId)?.Name} was removed";
                _highProvider.RemoveTeamFromTournament(team.TeamId, CupId);

            return RedirectToAction("Index", "Team");
        }

        public async Task<IActionResult> RegistrToCup(int CupId, string Password)
        {
            Team team = await CurrentTeam();

            if (Password == team.Password)
            {
                TempData["message"] = $"You have been registr for the {_highProvider.GetTournament(CupId).Name}";
                _highProvider.AddTeamToTournament(team.TeamId, CupId);
            }

            return RedirectToAction("Index", "Team");
        }
    }
}