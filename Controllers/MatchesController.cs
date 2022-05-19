using CricScore.Controllers.Dto;
using CricScore.Data;
using CricScore.Filters;
using CricScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricScore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : Controller
{
    private readonly CricScoreDbContext _dbContext;

    public MatchesController(CricScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [CheckUserIdHeader]
    public IActionResult ScheduleMatch([FromBody] ScheduleMatchDto dto)
    {
        if (dto.ScheduledDateTime < DateTime.Now)
        {
            return BadRequest(
                new ErrorDto("Match cannot be scheduled for a past datetime."));
        }

        var userIdStr = HttpContext.Request.Headers["userId"].First();
        var userId = int.Parse(userIdStr);

        var homeTeam = _dbContext.Teams.SingleOrDefault(t => t.Id == dto.HomeTeamId && t.UserId == userId);
        if (homeTeam == null)
        {
            return BadRequest(new ErrorDto("Home team id is invalid."));
        }

        var awayTeam = _dbContext.Teams.SingleOrDefault(t => t.Id == dto.AwayTeamId && t.UserId == userId);
        if (awayTeam == null)
        {
            return BadRequest(new ErrorDto("Away team id is invalid."));
        }

        var newMatch = new Match
        {
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            ScheduledDateTime = dto.ScheduledDateTime,
            UserId = userId
        };

        _dbContext.Matches.Add(newMatch);
        _dbContext.SaveChanges();

        return Created($"/matches/{newMatch.Id}", newMatch);
    }
}
