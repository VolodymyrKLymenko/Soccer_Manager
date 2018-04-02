using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer_Manager.ModelClasses
{
    public class Tournament
    {
        [Key]
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxCountTeams { get; set; }

        public List<Team> Teams { get; set; }

        public Tournament()
        {
            Teams = new List<Team>();
        }

    }

}
