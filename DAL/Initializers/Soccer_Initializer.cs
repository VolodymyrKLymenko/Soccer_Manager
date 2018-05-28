using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model_Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Initializers
{
    public static class Soccer_Initializer
    {
        public static void Initialize(SoccerContext context)
        {
            //roleManager.CreateAsync(new IdentityRole("Organizer"));
            //roleManager.CreateAsync(new IdentityRole("Team"));

            
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "Barcelona_1";
            barcelona.Mail = "barcelona@gmail.com";
            barcelona.DataCreation = new DateTime(1900, 12, 12);
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "Liverpool_1";
            liverpool.Mail = "liverpool@gmail.com";
            liverpool.DataCreation = new DateTime(1955, 5, 10);
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "Arsenal_1";
            arsenal.Mail = "arsenal@gmail.com";
            arsenal.DataCreation = new DateTime(1975, 3, 29);

            context.Teams.AddRange(new List<Team> { barcelona, liverpool, arsenal });
            context.SaveChanges();


            Player messi = new Player("Lionel", "Messi", "Middle attacker") { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender") { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward") { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker") { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker") { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker") { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward") { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper") { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder") { Team = arsenal };

            messi.Born = new DateTime(1985, 5, 5);
            pique.Born = new DateTime(1992, 5, 25);
            suarez.Born = new DateTime(1954, 5, 5);
            mane.Born = new DateTime(1993, 9, 15);
            salah.Born = new DateTime(2001, 5, 5);
            firmino.Born = new DateTime(2005, 5, 5);
            genrih.Born = new DateTime(1994, 3, 14);
            cech.Born = new DateTime(1998, 4, 18);
            mustafi.Born = new DateTime(1991, 7, 19);


            context.Players.AttachRange(
                new List<Player> { messi, pique, suarez, mane, salah, firmino, genrih, cech, mustafi });
            context.SaveChanges();


            Tournament APL = new Tournament();
            APL.Name = "APL";
            APL.MaxCountTeams = 18;
            APL.StartDate = "2017-01-10";
            APL.EndDate = "2018-08-08";
            APL.Password = "Aplapl_1";
            APL.Mail = "englishLeague@gmail.com";
            Tournament euroCup = new Tournament();
            euroCup.Name = "EURO_cup";
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "2017-02-08";
            euroCup.EndDate = "2018-05-05";
            euroCup.Password = "Eurocup_1";
            euroCup.Mail = "euro_cup@gmail.com";
            Tournament someCup = new Tournament();
            someCup.Name = "Some";
            someCup.MaxCountTeams = 30;
            someCup.StartDate = "2017-02-08";
            someCup.EndDate = "2018-05-05";
            someCup.Password = "Somesome_1";
            someCup.Mail = "some_cup@gmail.com";

            context.Add(APL);
            context.Add(euroCup);
            context.SaveChanges();
            context.Add(someCup);
            context.SaveChanges();


            APL.TeamTournaments.Add(new TeamTournament { Tournament = APL, Team = liverpool });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = APL });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = euroCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = someCup });
            someCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = someCup });

            context.SaveChanges();

            /*
            Team barca = _highProvider.GetAllTeam().FirstOrDefault(el => el.Name == "Barcelona");
            Team liver = _highProvider.GetAllTeam().FirstOrDefault(el => el.Name == "Liverpool");
            Team arsen = _highProvider.GetAllTeam().FirstOrDefault(el => el.Name == "Arsenal");
            DAL.Model_Classes.User userBarca = new DAL.Model_Classes.User { UserId = barca.TeamId, UserName = "Barcelona" };
            IdentityResult _result = await _userManager.CreateAsync(userBarca, barca.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userBarca, "Team");
            }
            DAL.Model_Classes.User userLiver = new DAL.Model_Classes.User { UserId = liver.TeamId, UserName = "Liverpool" };
            _result = await _userManager.CreateAsync(userLiver, liver.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userLiver, "Team");
            }
            DAL.Model_Classes.User userArsen = new DAL.Model_Classes.User { UserId = arsen.TeamId, UserName = "Arsenal" };
            _result = await _userManager.CreateAsync(userArsen, arsen.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userArsen, "Team");
            }

            Tournament apl = _highProvider.GetAllTournaments().FirstOrDefault(el => el.Name == "APL");
            Tournament euro = _highProvider.GetAllTournaments().FirstOrDefault(el => el.Name == "EURO_cup");
            Tournament some = _highProvider.GetAllTournaments().FirstOrDefault(el => el.Name == "Some");
            DAL.Model_Classes.User userAPL = new DAL.Model_Classes.User { UserId = apl.TournamentId, UserName = "APL" };
            _result = await _userManager.CreateAsync(userAPL, apl.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userAPL, "Organizer");
            }
            DAL.Model_Classes.User userEuro = new DAL.Model_Classes.User { UserId = euro.TournamentId, UserName = "EURO_cup" };
            _result = await _userManager.CreateAsync(userEuro, euro.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userEuro, "Organizer");
            }
            DAL.Model_Classes.User userSome = new DAL.Model_Classes.User { UserId = some.TournamentId, UserName = "Some" };
            _result = await _userManager.CreateAsync(userSome, some.Password);
            if (_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userSome, "Organizer");
            }
            */
        }
    }
}
