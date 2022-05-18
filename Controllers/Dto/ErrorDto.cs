namespace CricScore.Controllers.Dto;

public class ErrorDto
{
    public ErrorDto()
    {
        Message = "An error occurred";
    }

    public ErrorDto(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}
