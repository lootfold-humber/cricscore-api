using CricScore.Controllers.Dto;
using CricScore.Data;
using CricScore.Filters;
using CricScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricScore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : Controller
{
    private readonly CricScoreDbContext _dbContext;

    public TeamsController(CricScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [CheckUserIdHeader]
    public IActionResult AddTeam([FromBody] Team team)
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var teamInDb = _dbContext.Teams.SingleOrDefault(t => t.Name == team.Name);
        if (teamInDb != null)
        {
            return BadRequest(new ErrorDto("Duplicate team name"));
        }

        team.UserId = int.Parse(userId);

        _dbContext.Teams.Add(team);
        _dbContext.SaveChanges();

        return Created($"/teams/{team.Id}", team);
    }

    [HttpGet]
    [CheckUserIdHeader]
    public IActionResult GetAllForUser()
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var teams = _dbContext.Teams.Where(t => t.UserId == int.Parse(userId));

        return Ok(teams);
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var teams = _dbContext.Teams;

        return Ok(teams);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [CheckUserIdHeader]
    public IActionResult DeleteTeamForUser([FromRoute] int id)
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var teamInDb = _dbContext.Teams.SingleOrDefault(
            t => t.Id == id && t.UserId == int.Parse(userId));
        if (teamInDb == null)
        {
            return NotFound();
        }

        _dbContext.Teams.Remove(teamInDb);
        _dbContext.SaveChanges();

        return Ok();
    }
}
