using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.Model_Classes;

namespace DAL
{
    public class SoccerContext : DbContext
    {
        public SoccerContext(DbContextOptions<SoccerContext> options) : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamTournament>()
                .HasKey(t => new { t.TournamentId, t.TeamId });

            modelBuilder.Entity<TeamTournament>()
                .HasOne(sc => sc.Tournament)
                .WithMany(c => c.TeamTournaments)
                .HasForeignKey(sc => sc.TeamId);

            modelBuilder.Entity<TeamTournament>()
                .HasOne(sc => sc.Team)
                .WithMany(s => s.TeamTournaments)
                .HasForeignKey(sc => sc.TournamentId);
        }

    }
}
