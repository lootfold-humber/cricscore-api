using CricScore.Controllers.Dto;
using CricScore.Data;
using CricScore.Filters;
using CricScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricScore.Controllers;

[ApiController]
[Route("api/[controller]")]
[CheckUserIdHeader]
public class TeamsController : Controller
{
    private readonly CricScoreDbContext _dbContext;

    public TeamsController(CricScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
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
    public IActionResult GetAllForUser()
    {
        var userId = HttpContext.Request.Headers["userId"].First();

        var teams = _dbContext.Teams.Where(t => t.UserId == int.Parse(userId));

        return Ok(teams);
    }
}
