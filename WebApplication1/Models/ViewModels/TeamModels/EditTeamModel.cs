using DAL.Model_Classes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels.TeamModels
{
    public class EditTeamModel
    {
        public Team Team { get; set; }

        public IFormFile File { get; set; }
    }
}
