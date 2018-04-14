using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DAL.Model_Classes;

namespace WebApplication1.Models
{
    public class User
    {
        public bool IsTeam { get; set; }
        public Tournament Tournament { get; set; }
        public Team Team { get; set; }

        public virtual void SetTournament(Tournament tournament)
        {
            Tournament = tournament;
        }
        public virtual void SetTeam(Team team)
        {
            Team = team;
        }
        public virtual void SetType(bool type)
        {
            IsTeam = type;
        }
        public virtual void Clear()
        {
            IsTeam = false;
            Tournament = null;
            Team = null;
        }
    }

}
