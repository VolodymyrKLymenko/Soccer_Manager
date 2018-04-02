using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ModelClasses;

namespace DAL.DataAccess
{

    public class SoccerContext : DbContext
    {
        public SoccerContext(DbContextOptions<SoccerContext> options) : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamTournament>()
                .HasKey(t => new { t.TournamentId, t.TeamId });

            modelBuilder.Entity<TeamTournament>()
                .HasOne(sc => sc.Teams)
                .WithMany(s => s.TeamTournaments)
                .HasForeignKey(sc => sc.TournamentId);

            modelBuilder.Entity<TeamTournament>()
                .HasOne(sc => sc.Tournaments)
                .WithMany(c => c.TeamTournaments)
                .HasForeignKey(sc => sc.TeamId);
        }

    }

}