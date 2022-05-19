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
    public DbSet<TossDecision> TossDecisions => Set<TossDecision>();
    public DbSet<Toss> Tosses => Set<Toss>();

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

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Toss)
            .WithOne(t => t.Match)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Toss>()
            .HasOne(t => t.Match)
            .WithOne(m => m.Toss)
            .HasForeignKey<Toss>(t => t.MatchId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TossDecision>().HasData(new List<TossDecision>{
                new TossDecision{Id = 1, Value = "Bat"},
                new TossDecision{Id = 2, Value = "Bowl"}});
    }
}
