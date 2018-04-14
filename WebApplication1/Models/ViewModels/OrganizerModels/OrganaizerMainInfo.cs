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
        public List<Team> Teams { get; set; }
    }
}
