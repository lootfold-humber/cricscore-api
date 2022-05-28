using CricScore.Data;
using CricScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricScore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : Controller
{
    private readonly CricScoreDbContext _dbContext;

    public ScoresController(CricScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult AddUpdateScore([FromBody] Score score)
    {
        var scoreInDb = _dbContext.Scores.SingleOrDefault(
            s => s.MatchId == score.MatchId && s.Innings == score.Innings);

        if (scoreInDb != null)
        {
            scoreInDb.Runs = score.Runs;
            scoreInDb.Wickets = score.Wickets;
            scoreInDb.Overs = score.Overs;
            scoreInDb.Balls = score.Balls;
            scoreInDb.MaxOvers = score.MaxOvers;
        }
        else
        {
            _dbContext.Scores.Add(score);
        }

        _dbContext.SaveChanges();

        return Ok();
    }
}
