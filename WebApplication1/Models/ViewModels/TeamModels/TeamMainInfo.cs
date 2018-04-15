using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DAL.Model_Classes;

namespace WebApplication1.Models.ViewModels
{
    public class TeamMainInfo
    {
        public Team Team { get; set; }
        public bool ShowConfirming { get; set; }

        public TeamMainInfo()
        {
            ShowConfirming = false;
        }
    }
}
