using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DAL.Model_Classes;

namespace WebApplication1.Models.ViewModels
{
    public class OrganaizerMainInfo
    {
        public Tournament Tournament { get; set; }
        public Team SelectedTeam { get; set; }
        public bool ShowConfirming { get; set; }
        public List<Team> Teams = new List<Team>();

        public OrganaizerMainInfo()
        {
            ShowConfirming = false;
        }
    }
}
