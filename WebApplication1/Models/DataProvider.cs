using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DataProvider
    {
        /*
        public List<Team> teams = new List<Team>();
        public List<Tournament> tournaments = new List<Tournament>();
        public List<Player> players = new List<Player>();

        public DataProvider()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            Team barcelona = new Team();
            Team liverpool = new Team();
            Team arsenal = new Team();

            barcelona.Name = "Barcelona";
            barcelona.Tournaments = new List<Tournament>();
            barcelona.Players = new List<Player>()
            {
                new Player("Lionel", "Messi", "Middle attacker", 30, 1, barcelona),
                new Player("Adam", "Pique", "Defender", 25, 2, barcelona),
                new Player("Peter", "Suarez", "Forward", 28, 3, barcelona)
            };
            barcelona.TeamId = 1;

            liverpool.Name = "Liverpool";
            liverpool.Players = new List<Player>()
            {
                new Player("Sadio", "Mane", "Middle attacker", 24, 7, liverpool),
                new Player("Muhamed", "Salah", "Middle attacker", 25, 8, liverpool),
                new Player("Roberto", "Firmino", "Attacker", 27, 9, liverpool)
            };
            liverpool.Tournaments = new List<Tournament>();
            liverpool.TeamId = 2;

            arsenal.Name = "Arsenal";
            arsenal.Players = new List<Player>()
            {
                new Player("Andriy", "Yarmolenko", "Forward", 30 , 4, arsenal),
                new Player("Evgen", "Konoplyanka", "Forward", 25, 5, arsenal),
                new Player("Bob", "Wolcat", "Defemder", 33, 6, arsenal)
            };
            arsenal.Tournaments = new List<Tournament>();
            arsenal.TeamId = 3;


            Tournament APL = new Tournament();
            APL.Name = "English premier league";
            APL.MaxCountTeams = 18;
            APL.StartDate = "01.10.2017";
            APL.EndDate = "08.08.2018";
            APL.TournamentId = 1;
            APL.Teams.Add(liverpool);
            APL.Teams.Add(arsenal);

            Tournament euroCup = new Tournament();
            euroCup.Name = "UEFA CUP";
            euroCup.MaxCountTeams = 2;
            euroCup.StartDate = "02.08.2017";
            euroCup.EndDate = "05.05.2018";
            euroCup.TournamentId = 2;
            euroCup.Teams.Add(barcelona);
            euroCup.Teams.Add(liverpool);

            teams.Add(barcelona);
            teams.Add(liverpool);
            teams.Add(arsenal);

            tournaments.Add(APL);
            tournaments.Add(euroCup);

            foreach (Player player in barcelona.Players)
            {
                players.Add(player);
            }
            foreach (Player player in liverpool.Players)
            {
                players.Add(player);
            }
            foreach (Player player in arsenal.Players)
            {
                players.Add(player);
            }

        }*/

    }
}
