using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DAL.Model_Classes
{
    [Serializable]
    public class Tournament
    {
        public int TournamentId { get; set; }

        [Required(ErrorMessage = "Please enter a tournament name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a start date")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Please enter a end date")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "Please enter an age")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int MaxCountTeams { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [UIHint("password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a mail")]
        public string Mail { get; set; }

        public List<TeamTournament> TeamTournaments { get; set; }

        public Tournament()
        {
            TeamTournaments = new List<TeamTournament>();
        }

    }
}
