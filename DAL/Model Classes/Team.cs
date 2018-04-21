using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DAL.Model_Classes
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Please enter a team name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a mail")]
        public string Mail { get; set; }

        public string Password { get; set; }

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
