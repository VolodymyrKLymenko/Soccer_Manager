using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model_Classes;

namespace DAL.Initializers
{
    public static class Soccer_Initializer
    {
        public static void Initialize(SoccerContext context)
        {
            Team barcelona = new Team("Barcelona");
            barcelona.Password = "barcelona";
            barcelona.Mail = "barcelona@gmail.com";
            barcelona.DataCreation = new DateTime(1900, 12, 12);
            Team liverpool = new Team("Liverpool");
            liverpool.Password = "liverpool";
            liverpool.Mail = "liverpool@gmail.com";
            liverpool.DataCreation = new DateTime(1955, 5, 10);
            Team arsenal = new Team("Arsenal");
            arsenal.Password = "arsenal";
            arsenal.Mail = "arsenal@gmail.com";
            arsenal.DataCreation = new DateTime(1975, 3, 29);

            context.Teams.AddRange(new List<Team> { barcelona, liverpool, arsenal });
            context.SaveChanges();


            Player messi = new Player("Lionel", "Messi", "Middle attacker", 30) { Team = barcelona };
            Player pique = new Player("Adam", "Pique", "Defender", 25) { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward", 28) { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker", 24) { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker", 25) { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker", 27) { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward", 29) { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper", 32) { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder", 33) { Team = arsenal };

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

        }
    }
}
