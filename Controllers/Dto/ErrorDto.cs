namespace CricScore.Controllers.Dto;

public class ErrorDto
{
    public ErrorDto()
    {
        Message = "An error occurred";
    }

    public string Message { get; set; }
}
