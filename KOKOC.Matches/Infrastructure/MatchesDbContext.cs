using KOKOC.Matches.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KOKOC.Matches.Infrastructure
{
    public class MatchesDbContext : DbContext
    {
        public MatchesDbContext() : base() { }
        public MatchesDbContext(DbContextOptions<MatchesDbContext> opt) : base(opt) { }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Match>(cfg =>
            {
                cfg.HasKey(e => e.Id);
                cfg.HasOne(e => e.Stadium)
                    .WithMany()
                    .HasForeignKey(e => e.StadiumId);

                cfg.HasOne(e => e.OpponentTeam)
                    .WithMany()
                    .HasForeignKey(e => e.OpponentTeamId);

                cfg.HasOne(e => e.League)
                    .WithMany()
                    .HasForeignKey(e => e.LeagueId);

                cfg.ComplexProperty(e => e.Score);
            });
            builder.Entity<Team>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });
            builder.Entity<Stadium>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });
            builder.Entity<League>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });
        }

    }
}
