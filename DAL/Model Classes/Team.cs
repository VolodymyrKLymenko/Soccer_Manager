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
        public string Mail { get; set; }
        public string Password { get; set; }
        public List<Reward> Rewards { get; set; }
        public DateTime DataCreation { get; set; }
        public byte[] Avatar { get; set; }

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
