using CricScore.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CricScore.Filters;

public class CheckUserIdHeader : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userIdHeader = context.HttpContext.Request.Headers["userId"].FirstOrDefault();
        if (userIdHeader == null)
        {
            throw new ApiValidationException("Header \"userId\" is required.");
        }
    }
}
