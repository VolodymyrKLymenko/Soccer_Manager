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
            ViewResult result = (ViewResult)controller.Edit(new WebApplication1.Models.ViewModels.TeamModels.EditTeamModel() { Team = new_team, File = null}).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(new_team, ((TeamMainInfo)viewResult?.Model).Team);
        }

        [Fact]
        public void IndexReturnsAViewResultWithALists()
        {
            Tournament _cup = new Tournament();
            _cup.Name = "English premier league";
            _cup.MaxCountTeams = 18;
            _cup.TournamentId = 1;
            _cup.StartDate = "01.10.2017";
            _cup.EndDate = "08.08.2018";
            _cup.Password = "apl";
            _cup.Mail = "englishLeague@gmail.com";

            Tournament cup = _cup;
            _cup.Name = "New";
            _cup.TournamentId = 2;
            _cup.Password = "new";

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

            _team.Players = new List<Player>() { player };
            _team.Rewards = new List<Reward>() { reward };

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament> { cup, _cup });
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
            Assert.Equal(new List<Tournament>() { cup, _cup }, ((TeamMainInfo)viewResult?.Model).Cups);
            Assert.Equal(new List<Player>() { player }, ((TeamMainInfo)viewResult?.Model).Team.Players);
            Assert.Equal(new List<Reward>() { reward }, ((TeamMainInfo)viewResult?.Model).Team.Rewards);
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

        [Fact]
        public void AddPlayerTest()
        {
            Team _team = new Team();
            _team.Name = "Barcelona";
            _team.TeamId = 1;
            _team.Password = "barc";
            _team.Mail = "barc@gmail.com";

            Player player1 = new Player();
            player1.Name = "Messi";
            player1.PlayerId = 1;
            player1.Born = new DateTime(1987, 12, 23);
            player1.Position = "Middle Attacker";
            player1.Surname = "Lionel";
            player1.TeamId = 1;

            Player player2 = new Player();
            player2.Name = "Neymar";
            player2.PlayerId = 2;
            player2.Born = new DateTime(1989, 02, 13);
            player2.Position = "Middle Attacker";
            player2.Surname = "Id";
            player2.TeamId = 1;

            Reward reward = new Reward();
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            _team.Players = new List<Player>() { player1 };
            _team.Rewards = new List<Reward>() { reward };

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player1 });
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
            RedirectToActionResult result = (RedirectToActionResult)controller.AddPlayer(player2).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditPlayerTest()
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

            Player new_player = player;
            new_player.Name = "New Messi";
            new_player.Position = "Defence";

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
            RedirectToActionResult result = (RedirectToActionResult)controller.EditPlayer(new_player).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemovePlayerTest()
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

            Player new_player = player;
            new_player.PlayerId = 2;
            new_player.Name = "New Messi";
            new_player.Position = "Defence";

            Reward reward = new Reward();
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player, new_player });
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
            RedirectToActionResult result = (RedirectToActionResult)controller.RemovePlayer(new_player.PlayerId).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void AddRewardTest()
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

            Reward reward1 = new Reward();
            reward1.Name = "Reward1";
            reward1.Date = "1989-01-23";
            reward1.TeamId = 1;

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
            RedirectToActionResult result = (RedirectToActionResult)controller.AddReward(reward).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditRewardTest()
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

            Reward new_reward = reward;
            new_reward.Name = "New Reward";

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
            RedirectToActionResult result = (RedirectToActionResult)controller.EditReward(new_reward).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemoveRewardTest()
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
            reward.RewardId = 1;
            reward.Name = "Reward";
            reward.Date = "1987-01-23";
            reward.TeamId = 1;

            Reward new_reward = reward;
            new_reward.RewardId = 2;
            new_reward.Name = "New Reward";

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            var mockLowService = new Mock<ILowLevelSoccerManagmentService>();
            mockHighService.Setup(service => service.GetAllTeam()).Returns(new List<Team>() { _team });
            mockLowService.Setup(service => service.GetAllPlayers()).Returns(new List<Player>() { player });
            mockLowService.Setup(service => service.GetAllRewards()).Returns(new List<Reward>() { reward, new_reward });
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
            RedirectToActionResult result = (RedirectToActionResult)controller.RemoveReward(new_reward.RewardId).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemoveCupTest()
        {
            Tournament _cup = new Tournament();
            _cup.Name = "English premier league";
            _cup.MaxCountTeams = 18;
            _cup.TournamentId = 1;
            _cup.StartDate = "01.10.2017";
            _cup.EndDate = "08.08.2018";
            _cup.Password = "apl";
            _cup.Mail = "englishLeague@gmail.com";

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
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament> { _cup });
            mockHighService.Setup(service => service.AddTeamToTournament(_team.TeamId, _cup.TournamentId));
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
            RedirectToActionResult result = (RedirectToActionResult)controller.RemoveCup(_cup.TournamentId).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RegistrToCupTest()
        {
            Tournament _cup = new Tournament();
            _cup.Name = "English premier league";
            _cup.MaxCountTeams = 18;
            _cup.TournamentId = 1;
            _cup.StartDate = "01.10.2017";
            _cup.EndDate = "08.08.2018";
            _cup.Password = "apl";
            _cup.Mail = "englishLeague@gmail.com";

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
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament> { _cup });
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
            RedirectToActionResult result = (RedirectToActionResult)controller.RegistrToCup(_cup.TournamentId, _team.Password).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
