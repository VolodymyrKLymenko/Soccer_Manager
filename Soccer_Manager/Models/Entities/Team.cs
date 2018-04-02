using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer_Manager.ModelClasses
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }

        public List<Tournament> Tournaments { get; set; }
        public List<Player> Players { get; set; }

        public Team()
        {
            Tournaments = new List<Tournament>();
        }

    }

}
