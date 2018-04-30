using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model_Classes
{
    [Serializable]
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxCountTeams { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public List<TeamTournament> TeamTournaments { get; set; }

        public Tournament()
        {
            TeamTournaments = new List<TeamTournament>();
        }

    }
}
