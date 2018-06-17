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
    public class AccountControllerTests
    {
        [Fact]
        public async void RegisterTeamTest()
        {
            var testTeams = new List<Team>() { new Team() { Name = "Name1", TeamId = 1, Password = "Name_1" } };
            var newTeam = new Team() { Name = "Name2", TeamId = 2, Password = "Name_2" };

            var mockHighProvider = new Mock<IHighLevelSoccerManagerService>();
            mockHighProvider.Setup(service => service.CreateTeam(It.IsAny<Team>())).Callback<Team>(team => testTeams.Add(team));

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var res = new Team();
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
               .Callback<string>((str) => { res = testTeams.FirstOrDefault(el => el.Name == str); })
               .Returns(Task.FromResult(new User() { UserName = res.Name, UserId = res.TeamId }));
            userManager.Setup(ser => ser.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var mockSignInManager = new Mock<SignInManager<User>>();
            mockSignInManager.Setup(service => service.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            AccountController controller = new AccountController(mockHighProvider.Object, userManager.Object,null, null);

            var result = await controller.RegisterTeam(new WebApplication1.Models.ViewModels.TeamModels.RegisterModel() { Name = "Name2", Password = "Name_2"});

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Contains<Team>(testTeams, el => el.Name == "Name2");
            Assert.Contains<Team>(testTeams, el => el.Name == "Name1");
            Assert.Equal(2, testTeams.Count);
        }

        [Fact]
        public async void RegisterCupTest()
        {
            var testCups = new List<Tournament>() { new Tournament() { Name = "Name1", TournamentId = 1, Password = "Name_1" } };
            var newCup = new Tournament() { Name = "Name2", TournamentId = 2, Password = "Name_2" };

            var mockHighProvider = new Mock<IHighLevelSoccerManagerService>();
            mockHighProvider.Setup(service => service.CreateTournament(It.IsAny<Tournament>())).Callback<Tournament>(cup => testCups.Add(cup));

            var store = new Mock<IUserStore<User>>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var res = new Tournament();
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
               .Callback<string>((str) => { res = testCups.FirstOrDefault(el => el.Name == str); })
               .Returns(Task.FromResult(new User() { UserName = res.Name, UserId = res.TournamentId }));
            userManager.Setup(ser => ser.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var mockSignInManager = new Mock<SignInManager<User>>();
            mockSignInManager.Setup(service => service.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            AccountController controller = new AccountController(mockHighProvider.Object, userManager.Object, null, null);

            var result = await controller.RegisterCup(new WebApplication1.Models.ViewModels.OrganizerModels.RegisterOrganizerModel() { Name = "Name2", Password = "Name_2" });

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Contains<Tournament>(testCups, el => el.Name == "Name2");
            Assert.Contains<Tournament>(testCups, el => el.Name == "Name1");
            Assert.Equal(2, testCups.Count);
        }
    }
}
