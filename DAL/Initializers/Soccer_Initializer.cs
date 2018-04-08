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
            Team liverpool = new Team("Liverpool");
            Team arsenal = new Team("Arsenal");

            context.Teams.AddRange(new List<Team> { barcelona, liverpool, arsenal });
            context.SaveChanges();


            Player messi = new Player("Lionel", "Messi", "Middle attacker", 30) { Team = barcelona};
            Player pique = new Player("Adam", "Pique", "Defender", 25) { Team = barcelona };
            Player suarez = new Player("Peter", "Suarez", "Forward", 28) { Team = barcelona };
            Player mane = new Player("Sadio", "Mane", "Middle attacker", 24) { Team = liverpool };
            Player salah = new Player("Muhamed", "Salah", "Middle attacker", 25) { Team = liverpool };
            Player firmino = new Player("Roberto", "Firmino", "Attacker", 27) { Team = liverpool };
            Player genrih = new Player("Genrih", "Mikhitarian", "Forward", 29) { Team = arsenal };
            Player cech = new Player("Peter", "Cech", "Goalkeeper", 32) { Team = arsenal };
            Player mustafi = new Player("Skodran", "Mustafi", "Defemder", 33) { Team = arsenal };

            context.Players.AttachRange(
                new List<Player> { messi, pique, suarez, mane, salah, firmino, genrih, cech, mustafi });
            context.SaveChanges();


            Tournament APL = new Tournament();
            APL.Name = "English premier league";
            APL.MaxCountTeams = 18;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            Tournament euroCup = new Tournament();
            euroCup.Name = "UEFA CUP";
            euroCup.MaxCountTeams = 30;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";

            context.Add(APL);
            context.Add(euroCup);
            context.SaveChanges();


            APL.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = APL });
            APL.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = APL });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = liverpool, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = arsenal, Tournament = euroCup });
            euroCup.TeamTournaments.Add(new TeamTournament { Team = barcelona, Tournament = euroCup });

            context.SaveChanges();

        }
    }
}
