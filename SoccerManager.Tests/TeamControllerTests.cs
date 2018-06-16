using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;
using Xunit;
using Services;
using Moq;
using System.Collections.Generic;
using DAL.Model_Classes;
using WebApplication1.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace SoccerManager.Tests
{
    public class TeamControllerTests
    {
        private IQueryable<Player> GetTestPlayers()
        {
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";

            Player messi = new Player("Lionel", "Messi", "Middle attacker") { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender") { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward") { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker") { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker") { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker") { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward") { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper") { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder") { Team = arsenal };

            Tournament APL = new Tournament();
            APL.Name = "English premier league";
            APL.MaxCountTeams = 18;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            APL.Password = "apl";
            APL.Mail = "englishLeague@gmail.com";
            Tournament euroCup = new Tournament();
            euroCup.Name = "EURO cup";
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";
            euroCup.Password = "eurocup";
            euroCup.Mail = "euro_cup@gmail.com";
            Tournament someCup = new Tournament();
            someCup.Name = "Some";
            someCup.MaxCountTeams = 30;
            someCup.StartDate = "02.08.2017";
            someCup.EndDate = "05.05.2018";
            someCup.Password = "some";
            someCup.Mail = "some_cup@gmail.com";

            APL.TeamTournaments.Add(new TeamTournament { Tournament = APL, Team = liverpool });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = APL });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = euroCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = someCup });

            var players = new List<Player>
            {
                messi, pique, suarez, mane, salah, firmino, genrih, cech, mustafi
            };
            return players.AsQueryable();
        }
        private IQueryable<Team> GetTestTeams()
        {
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";

            Player messi = new Player("Lionel", "Messi", "Middle attacker") { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender") { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward") { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker") { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker") { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker") { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward") { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper") { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder") { Team = arsenal };

            Tournament APL = new Tournament();
            APL.Name = "English premier league";
            APL.MaxCountTeams = 18;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            APL.Password = "apl";
            APL.Mail = "englishLeague@gmail.com";
            Tournament euroCup = new Tournament();
            euroCup.Name = "EURO cup";
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";
            euroCup.Password = "eurocup";
            euroCup.Mail = "euro_cup@gmail.com";
            Tournament someCup = new Tournament();
            someCup.Name = "Some";
            someCup.MaxCountTeams = 30;
            someCup.StartDate = "02.08.2017";
            someCup.EndDate = "05.05.2018";
            someCup.Password = "some";
            someCup.Mail = "some_cup@gmail.com";

            APL.TeamTournaments.Add(new TeamTournament { Tournament = APL, Team = liverpool });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = APL });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = euroCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = someCup });

            var teams = new List<Team>
            {
                barcelona, liverpool, arsenal
            };
            return teams.AsQueryable();
        }
        private IQueryable<Tournament> GetTestCups()
        {
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";

            Player messi = new Player("Lionel", "Messi", "Middle attacker") { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender") { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward") { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker") { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker") { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker") { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward") { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper") { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder") { Team = arsenal };

            Tournament APL = new Tournament();
            APL.Name = "English premier league";
            APL.MaxCountTeams = 18;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            APL.Password = "apl";
            APL.Mail = "englishLeague@gmail.com";
            Tournament euroCup = new Tournament();
            euroCup.Name = "EURO cup";
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";
            euroCup.Password = "eurocup";
            euroCup.Mail = "euro_cup@gmail.com";
            Tournament someCup = new Tournament();
            someCup.Name = "Some";
            someCup.MaxCountTeams = 30;
            someCup.StartDate = "02.08.2017";
            someCup.EndDate = "05.05.2018";
            someCup.Password = "some";
            someCup.Mail = "some_cup@gmail.com";

            APL.TeamTournaments.Add(new TeamTournament { Tournament = APL, Team = liverpool });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = APL });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = euroCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = someCup });

            var cups = new List<Tournament>
            {
                APL, euroCup, someCup
            };
            return cups.AsQueryable();
        }

        [Fact]
        public void EditCupReturnsViewResultWithCupModel()
        {
            Team _team = new Team();
            _team.Name = "Barcelona";
            _team.TeamId = 1;
            _team.Password = "barc";
            _team.Mail = "barc@gmail.com";

            Team new_team = _team;
            new_team.Name = "New";
            new_team.Mail = "new@gmail.com";

            Player player = new Player();
            player.Name = "Messi";
            player.PlayerId = 1;
            player.Born = new DateTime(1987, 12, 23);
            player.Position = "Middle Attacker";
            player.Surname = "Lionel";
            player.TeamId = 1;

            Reward reward = new Reward();
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockHighService.Setup(service => service.UpdateTeam(It.IsAny<int>(), It.IsAny<Team>()))
                .Callback(() =>
                {
                    _team = new_team;
                });
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player });
            mockLowService.Setup(service => service.GetAllRewards()).Returns(new List<Reward>() { reward });
            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Barcelona",
                    UserId = 1
                });
            TeamController controller = new TeamController(mockHighService.Object, mockLowService.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Test")
                    }))
                }
            };
            
            // Act
            ViewResult result = (ViewResult)controller.Edit(new_team).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexReturnsAViewResultWithALists()
        {
            Team _team = new Team();
            _team.Name = "Barcelona";
            _team.TeamId = 1;
            _team.Password = "barc";
            _team.Mail = "barc@gmail.com";

            Player player = new Player();
            player.Name = "Messi";
            player.PlayerId = 1;
            player.Born = new DateTime(1987, 12, 23);
            player.Position = "Middle Attacker";
            player.Surname = "Lionel";
            player.TeamId = 1;

            Reward reward = new Reward();
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player });
            mockLowService.Setup(service => service.GetAllRewards()).Returns(new List<Reward>() { reward });
            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Barcelona",
                    UserId = 1
                });
            TeamController controller = new TeamController(mockHighService.Object, mockLowService.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Test")
                    }))
                }
            };

            // Act
            ViewResult result = (ViewResult)controller.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(_team, ((TeamMainInfo)viewResult?.Model).Team);
            Assert.False(((TeamMainInfo)viewResult?.Model).ShowConfirming);
        }

        [Fact]
        public void DeleteTest()
        {
            Team team1 = new Team();
            team1.Name = "Barcelona";
            team1.TeamId = 1;
            team1.Password = "barc";
            team1.Mail = "barc@gmail.com";

            Team team2 = new Team()
            {
                Name = "New",
                Mail = "New@new.new"
            };
            team2.TeamId = 2;

            List<Team> lst = new List<Team>() { team1, team2 };

            Player player = new Player();
            player.Name = "Messi";
            player.PlayerId = 1;
            player.Born = new DateTime(1987, 12, 23);
            player.Position = "Middle Attacker";
            player.Surname = "Lionel";
            player.TeamId = 1;

            Reward reward = new Reward();
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(lst);
            mockHighService.Setup(ser => ser.RemoveTeam(It.IsAny<int>())).Callback(() => lst.RemoveAt(0));
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player });
            mockLowService.Setup(service => service.GetAllRewards()).Returns(new List<Reward>() { reward });

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Barcelona",
                    UserId = 1
                });
            TeamController controller = new TeamController(mockHighService.Object, mockLowService.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Test")
                    }))
                }
            };

            controller.ModelState.AddModelError("Name", "Required");

            // Act
            RedirectToActionResult result = (RedirectToActionResult)controller.Delete().Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
