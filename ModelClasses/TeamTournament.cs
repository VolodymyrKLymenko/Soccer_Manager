using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClasses
{
    public class TeamTournament
    {
        public int TeamId { get; set; }
        public Team Teams { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournaments { get; set; }
    }
}
