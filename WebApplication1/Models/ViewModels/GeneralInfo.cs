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
    }
}
