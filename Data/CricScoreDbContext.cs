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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasIndex(t => t.Name).IsUnique();
    }
}
