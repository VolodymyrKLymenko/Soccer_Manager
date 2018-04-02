using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelClasses
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }

        public List<Player> Players { get; set; }
        public List<TeamTournament> TeamTournaments { get; set; }

        public Team()
        {
            TeamTournaments = new List<TeamTournament>();
        }

    }

}
