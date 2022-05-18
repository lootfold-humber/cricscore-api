using CricScore.Controllers.Dto;
using CricScore.Data;
using CricScore.Exceptions;
using CricScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricScore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly CricScoreDbContext _dbContext;

    public UsersController(CricScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult SignUp([FromBody] User user)
    {
        var userInDb = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
        if (userInDb != null)
        {
            throw new ApiValidationException("Provided email already in use.");
        }

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Created($"/users/{user.Id}", user);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var userInDb = _dbContext.Users.SingleOrDefault(
            u => u.Email == dto.Email && u.Password == dto.Password);

        if (userInDb == null)
        {
            return BadRequest(new ErrorDto("Invalid Username or Password"));
        }

        return Ok(userInDb);
    }

    [HttpGet("available")]
    public IActionResult CheckEmailAvailability([FromQuery] string email)
    {
        var userInDb = _dbContext.Users.SingleOrDefault(u => u.Email == email);
        var dto = new EmailAvailabilityDto(userInDb == null);
        return base.Ok(dto);
    }
}
