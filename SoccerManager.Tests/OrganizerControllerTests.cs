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
    public class OrganizerControllerTests
    {
        private IEnumerable<Player> GetTestPlayers()
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
            return players;
        }
        private IEnumerable<Team> GetTestTeams()
        {
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            barcelona.DataCreation = new DateTime(1900, 10, 10);
            barcelona.TeamId = 1;
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            liverpool.DataCreation = new DateTime(1955, 10, 10);
            liverpool.TeamId = 2;
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";
            arsenal.DataCreation = new DateTime(1900, 10, 10);
            arsenal.TeamId = 3;

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
            APL.StartDate = "01.10.2018";
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
            return teams;
        }
        private IEnumerable<Tournament> GetTestCups()
        {
            Team barcelona = new Team("Barcelona");
            barcelona.TeamId = 1;
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.TeamId = 2;
            liverpool.Mail = "liverpool@gmail.com";
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.TeamId = 3;
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
            APL.TournamentId = 1;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            APL.Password = "apl";
            APL.Mail = "englishLeague@gmail.com";
            Tournament euroCup = new Tournament();
            euroCup.Name = "EURO cup";
            euroCup.TournamentId = 2;
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";
            euroCup.Password = "eurocup";
            euroCup.Mail = "euro_cup@gmail.com";
            Tournament someCup = new Tournament();
            someCup.Name = "Some";
            someCup.TournamentId = 3;
            someCup.MaxCountTeams = 30;
            someCup.StartDate = "02.08.2017";
            someCup.EndDate = "05.05.2018";
            someCup.Password = "some";
            someCup.Mail = "some_cup@gmail.com";

            APL.TeamTournaments.Add(new TeamTournament { Tournament = APL, TournamentId = APL.TournamentId, Team = liverpool, TeamId = liverpool.TeamId });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, TeamId = arsenal.TeamId, TournamentId = APL.TournamentId, Tournament = APL });
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
            return cups;
        }

        [Fact]
        public void EditCupReturnsViewResultWithCupModel()
        {
            Tournament old_cup = new Tournament();
            old_cup.Name = "English premier league";
            old_cup.MaxCountTeams = 18;
            old_cup.TournamentId = 1;
            old_cup.StartDate = "01.10.2017";
            old_cup.EndDate = "08.08.2018";
            old_cup.Password = "apl";
            old_cup.Mail = "englishLeague@gmail.com";
            Tournament new_cup = new Tournament()
            {
                Name = "New",
                Mail = "New@new.new",
                StartDate = "01-01-2018",
                EndDate = "01-01-2019",
                MaxCountTeams = 40
            };

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament>() { old_cup });
            mockHighService.Setup(service => service.UpdateTournament(It.IsAny<int>(), It.IsAny<Tournament>()))
                .Callback(() =>
                {
                    old_cup = new_cup;
                });
            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Euro_cup",
                    UserId = 1
                });
            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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
            RedirectToActionResult result = (RedirectToActionResult)controller.Edit(new_cup).Result;

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
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

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament>() { _cup });
            mockHighService.Setup(service => service.GetTeam(It.IsAny<int>())).Returns(new Team() { Name = "team", TeamId = 1});
            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "APL",
                    UserId = 1
                });
            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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
            ViewResult result = (ViewResult)controller.Index(1).Result;
            Team selectedTeam = controller.SelectedTeam;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(_cup, ((OrganaizerMainInfo)viewResult?.Model).Tournament);
            Assert.Equal("team", ((OrganaizerMainInfo)viewResult?.Model).SelectedTeam.Name);
            Assert.False(((OrganaizerMainInfo)viewResult?.Model).ShowConfirming);
            Assert.Equal(1, selectedTeam.TeamId);
            Assert.Equal("team", selectedTeam.Name);
        }

        [Fact]
        public void SelectTest()
        {
            Tournament cup1 = new Tournament();
            cup1.Name = "English premier league";
            cup1.MaxCountTeams = 18;
            cup1.TournamentId = 1;
            cup1.StartDate = "01.10.2017";
            cup1.EndDate = "08.08.2018";
            cup1.Password = "apl";
            cup1.Mail = "englishLeague@gmail.com";
            Tournament cup2 = new Tournament()
            {
                Name = "New",
                Mail = "New@new.new",
                StartDate = "01-01-2018",
                EndDate = "01-01-2019",
                MaxCountTeams = 40
            };

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament>() { cup1 });
            mockHighService.Setup(ser => ser.GetAllTeam()).Returns(GetTestTeams());
            mockHighService.Setup(ser => ser.GetTeam(It.IsAny<int>())).Returns(GetTestTeams().First());

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Euro_cup",
                    UserId = 1
                });
            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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
            ViewResult result = (ViewResult)controller.SelectDate("1900", 1).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrganaizerMainInfo>(viewResult.Model);
            Assert.Equal(3, model.Teams.Count);
            Assert.Equal(2, model.SelectedTeams.Count);
            Assert.Equal(GetTestTeams().First().TeamId, model.SelectedTeam.TeamId);
        }

        [Fact]
        public async void DeleteTest()
        {
            Tournament cup1 = new Tournament();
            cup1.Name = "English premier league";
            cup1.MaxCountTeams = 18;
            cup1.TournamentId = 1;
            cup1.StartDate = "01.10.2017";
            cup1.EndDate = "08.08.2018";
            cup1.Password = "apl";
            cup1.Mail = "englishLeague@gmail.com";
            Tournament cup2 = new Tournament()
            {
                Name = "New",
                Mail = "New@new.new",
                StartDate = "01-01-2018",
                EndDate = "01-01-2019",
                MaxCountTeams = 40
            };
            cup2.TournamentId = 2;

            List<Tournament> lst = new List<Tournament>() {cup1, cup2};

            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(lst);
            mockHighService.Setup(ser => ser.RemoveTournament(It.IsAny<int>())).Callback(() =>
            {
                lst.RemoveAt(0);
            });

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "Euro_cup",
                    UserId = 1
                });
            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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
            var result = await controller.Delete();

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Single(lst);
        }

        [Fact]
        public async void RemoveTeamTest()
        {
            var testTeam = this.GetTestCups();

            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.RemoveTeamFromTournament(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>(
                (a, b) =>
                {
                    foreach (var team in testTeam)
                    {
                        if (team.TournamentId == b)
                        {
                            team.TeamTournaments.RemoveAll(e => e.TeamId == a);
                        }
                    }
                });
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(testTeam);
            mockHighService.Setup(service => service.GetTeam(It.IsAny<int>())).Returns<int>(a =>
            {
                return new Team() { Name = "Name" };
            });

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "APL",
                    UserId = 1
                });

            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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

            var result = await controller.RemoveTeam(2);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrganaizerMainInfo>(viewResult.Model);
            Assert.Empty(model.Tournament.TeamTournaments.Where(el => el.TeamId == 2));
            Assert.Single(model.Tournament.TeamTournaments);
            Assert.Equal(3, model.Tournament.TeamTournaments.Find(el => el.TournamentId == 1).TeamId);
        }

        [Fact]
        public async void EditCupTest()
        {
            var testCup = new Tournament() {
                Name = "APL",
                Mail = "Mail@mail.mail",
                TournamentId = 1,
                Password = "1111",
                MaxCountTeams = 10,
                EndDate = "end",
                StartDate = "start"
            };
            var updatedCup = new Tournament()
            {
                Name = "UAPL",
                Mail = "UMail@mail.mail",
                TournamentId = 1,
                Password = "U1111",
                MaxCountTeams = 100,
                EndDate = "Uend",
                StartDate = "Ustart"
            };


            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.UpdateTournament(It.IsAny<int>(), It.IsAny<Tournament>())).Callback<int, Tournament>(
                (a, b) =>
                {
                    testCup = b;
                });
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament>() { testCup});
            mockHighService.Setup(service => service.GetTeam(It.IsAny<int>())).Returns<int>(a =>
            {
                return new Team() { Name = "Name" };
            });

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "APL",
                    UserId = 1
                });

            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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

            await controller.Edit();
            var result = await controller.Edit(updatedCup, true);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrganaizerMainInfo>(viewResult.Model);
            Assert.Equal(model.Tournament.Name, updatedCup.Name);
            Assert.Equal(model.Tournament.MaxCountTeams, updatedCup.MaxCountTeams);
            Assert.Equal(model.Tournament.Mail, updatedCup.Mail);
            Assert.Equal(model.Tournament.EndDate, updatedCup.EndDate);
            Assert.Equal(model.Tournament.StartDate, updatedCup.StartDate);
            Assert.Equal(model.Tournament.Password, updatedCup.Password);
        }

        [Fact]
        public async void ConfirmTest()
        {
            var testCup = new Tournament()
            {
                Name = "APL",
                Mail = "Mail@mail.mail",
                TournamentId = 1,
                Password = "1111",
                MaxCountTeams = 10,
                EndDate = "end",
                StartDate = "start"
            };
            var updatedCup = new Tournament()
            {
                Name = "UAPL",
                Mail = "UMail@mail.mail",
                TournamentId = 1,
                Password = "U1111",
                MaxCountTeams = 100,
                EndDate = "Uend",
                StartDate = "Ustart"
            };


            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.UpdateTournament(It.IsAny<int>(), It.IsAny<Tournament>())).Callback<int, Tournament>(
                (a, b) =>
                {
                    testCup = b;
                });
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(new List<Tournament>() { testCup });
            mockHighService.Setup(service => service.GetTeam(It.IsAny<int>())).Returns<int>(a =>
            {
                return new Team() { Name = "Name" };
            });

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new User()
                {
                    UserName = "APL",
                    UserId = 1
                });

            OrganizerController controller = new OrganizerController(mockHighService.Object, userManager.Object);
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

            var result = await controller.Confirm();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrganaizerMainInfo>(viewResult.Model);
            Assert.Equal(model.Tournament.Name, testCup.Name);
            Assert.Equal(model.Tournament.MaxCountTeams, testCup.MaxCountTeams);
            Assert.Equal(model.Tournament.Mail, testCup.Mail);
            Assert.Equal(model.Tournament.EndDate, testCup.EndDate);
            Assert.Equal(model.Tournament.StartDate, testCup.StartDate);
            Assert.Equal(model.Tournament.Password, testCup.Password);
        }
    }
}
