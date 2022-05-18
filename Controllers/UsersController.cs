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
}
