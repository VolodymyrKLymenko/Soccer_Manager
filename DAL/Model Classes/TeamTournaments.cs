using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model_Classes
{
    public class TeamTournament
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}
