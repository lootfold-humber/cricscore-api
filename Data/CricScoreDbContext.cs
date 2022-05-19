using CricScore.Models;
using Microsoft.EntityFrameworkCore;

namespace CricScore.Data;

public class CricScoreDbContext : DbContext
{
    public CricScoreDbContext(DbContextOptions<CricScoreDbContext> options)
    : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasIndex(t => t.Name).IsUnique();

        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.WinningTeam)
            .WithMany(t => t.MatchesWon)
            .HasForeignKey(m => m.WinningTeamId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
