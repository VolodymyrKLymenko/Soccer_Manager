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

            Player messi = new Player("Lionel", "Messi", "Middle attacker", 30) { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender", 25) { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward", 28) { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker", 24) { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker", 25) { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker", 27) { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward", 29) { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper", 32) { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder", 33) { Team = arsenal };

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
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";

            Player messi = new Player("Lionel", "Messi", "Middle attacker", 30) { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender", 25) { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward", 28) { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker", 24) { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker", 25) { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker", 27) { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward", 29) { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper", 32) { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder", 33) { Team = arsenal };

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
            return teams;
        }
        private IEnumerable<Tournament> GetTestCups()
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

            Player messi = new Player("Lionel", "Messi", "Middle attacker", 30) { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender", 25) { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward", 28) { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker", 24) { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker", 25) { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker", 27) { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward", 29) { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper", 32) { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder", 33) { Team = arsenal };

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
            return cups;
        }

        [Fact]
        public void EditCupReturnsViewResultWithCupModel()
        {
            // Arrange
            var mockHighService = new Mock<IHighLevelSoccerManagerService>();
            mockHighService.Setup(service => service.GetAllTournaments()).Returns(GetTestCups());
            OrganizerController controller = new OrganizerController(mockHighService.Object);
            AccountController accountController = new AccountController(mockHighService.Object);

            //accountController.Login(new LoginModel() { Name = "EURO cup", Password = "eurocup", UserType = UserType.Organizer });

            var mockIIdentyty = new Mock<IIdentity>();
            mockIIdentyty.Setup(iden => iden.Name).Returns("faasf");
            mockIIdentyty.Setup(iden => iden.IsAuthenticated).Returns(true);

            //accountController.Authenticate();

            //identity.IsAuthenticated.BeFalse();




        controller.ModelState.AddModelError("Name", "Required");

            Tournament newCup = new Tournament();

            // Act
            var result = controller.Edit(newCup);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newCup, ((OrganaizerMainInfo)viewResult?.Model).Tournament);
            Assert.Null(((OrganaizerMainInfo)viewResult?.Model).SelectedTeam);
            Assert.False(((OrganaizerMainInfo)viewResult?.Model).ShowConfirming);
        }
    }
}
