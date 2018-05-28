using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using DAL.Model_Classes;

namespace WebApplication1.Models.ViewModels
{
    public class GeneralInfo
    {
        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public int MiddleAge { get; set; }


        public void RecalculateAge()
        {
            foreach (var item in Players)
            {
                item.Age_ = Age(item.Born);
            }
        }

        public float CalculateMiddleAge()
        {
            int res = 0;

            foreach (var item in Players)
            {
                res += Age(item.Born);
            }

            return res/Players.Count();
        }

        public float CalculateMiddleAgeTeam(List<Player> players)
        {
            if(players.Count() == 0) { return 0; }
            int res = 0;

            foreach (var item in players)
            {
                res += Age(item.Born);
            }

            return res / players.Count();
        }

        private static int Age(DateTime date)
        {
            return DateTime.Now.Year - date.Year;
        }
    }
}
