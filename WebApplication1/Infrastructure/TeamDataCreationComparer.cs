using DAL.Model_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure
{
    public class TeamDataEqualityCreationComparer : IEqualityComparer<Team>
    {
        public bool Equals(Team x, Team y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Team obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
