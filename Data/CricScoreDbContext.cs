using Microsoft.EntityFrameworkCore;

namespace CricScore.Data;

public class CricScoreDbContext : DbContext
{
    public CricScoreDbContext(DbContextOptions<CricScoreDbContext> options)
    : base(options)
    {

    }
}
