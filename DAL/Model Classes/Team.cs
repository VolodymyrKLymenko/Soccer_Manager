using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model_Classes
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }

        public List<Player> Players { get; set; }
        public List<TeamTournament> TeamTournaments { get; set; }

        public Team()
        {
        }

        public Team(string name)
        {
            Name = name;
        }

    }
}
