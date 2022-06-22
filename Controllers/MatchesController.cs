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

    /// <summary>
    /// Schedules the match.
    /// </summary>
    /// <param name="dto">The dto.</param>
    /// <returns></returns>
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

        return Ok();
    }

    /// <summary>
    /// Gets all matches.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllMatches()
    {
        return Ok(_dbContext.Matches);
    }

    /// <summary>
    /// Gets the toss for match.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpGet("{id:int}/toss")]
    public IActionResult GetTossForMatch([FromRoute] int id)
    {
        return Ok(_dbContext.Tosses.SingleOrDefault(t => t.MatchId == id));
    }

    /// <summary>
    /// Starts the match.
    /// </summary>
    /// <param name="matchId">The match identifier.</param>
    /// <param name="toss">The toss.</param>
    /// <returns></returns>
    [HttpPost("{matchId:int}/start")]
    [CheckUserIdHeader]
    public IActionResult StartMatch([FromRoute] int matchId, [FromBody] Toss toss)
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var matchInDb = _dbContext.Matches
            .SingleOrDefault(m => m.Id == matchId && m.UserId == int.Parse(userId));

        if (matchInDb == null)
        {
            return NotFound(new ErrorDto("Match not found."));
        }

        if (toss.WinningTeamId != matchInDb.AwayTeamId
            && toss.WinningTeamId != matchInDb.HomeTeamId)
        {
            return BadRequest(new ErrorDto("Invalid winning team Id."));
        }

        var tossDecision = _dbContext.TossDecisions.SingleOrDefault(t => t.Id == toss.TossDecisionId);
        if (tossDecision == null)
        {
            return BadRequest(new ErrorDto("Invalid toss decision."));
        }

        var tossInDb = _dbContext.Tosses.SingleOrDefault(t => t.MatchId == matchId);
        if (tossInDb != null)
        {
            _dbContext.Remove(tossInDb);
        }

        toss.MatchId = matchId;
        _dbContext.Tosses.Add(toss);
        _dbContext.SaveChanges();

        return Ok();
    }

    /// <summary>
    /// Completes the match.
    /// </summary>
    /// <param name="matchId">The match identifier.</param>
    /// <param name="completeMatchDto">The complete match dto.</param>
    /// <returns></returns>
    [HttpPost("{matchId:int}/complete")]
    [CheckUserIdHeader]
    public IActionResult CompleteMatch([FromRoute] int matchId, [FromBody] CompleteMatchDto completeMatchDto)
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var matchInDb = _dbContext.Matches
            .SingleOrDefault(m => m.Id == matchId && m.UserId == int.Parse(userId));

        if (matchInDb == null)
        {
            return NotFound(new ErrorDto("Match not found."));
        }

        if (completeMatchDto.WinningTeamId != matchInDb.AwayTeamId
            && completeMatchDto.WinningTeamId != matchInDb.HomeTeamId)
        {
            return BadRequest(new ErrorDto("Invalid winning team Id."));
        }

        matchInDb.WinningTeamId = completeMatchDto.WinningTeamId;
        _dbContext.SaveChanges();

        return Ok();
    }

    /// <summary>
    /// Deletes the match.
    /// </summary>
    /// <param name="matchId">The match identifier.</param>
    /// <returns></returns>
    [HttpDelete("{matchId:int}")]
    [CheckUserIdHeader]
    public IActionResult DeleteMatch([FromRoute] int matchId)
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var matchInDb = _dbContext.Matches
            .SingleOrDefault(m => m.Id == matchId && m.UserId == int.Parse(userId));

        if (matchInDb == null)
        {
            return NotFound(new ErrorDto("Match not found."));
        }

        _dbContext.Matches.Remove(matchInDb);
        _dbContext.SaveChanges();

        return Ok();
    }
}
