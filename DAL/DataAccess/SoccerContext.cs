using System;
using Microsoft.EntityFrameworkCore;
using ModelClasses;

namespace DAL.DataAccess
{
    public class SoccerContext : DbContext
    {
        public SoccerContext() : base()
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}