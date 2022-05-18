namespace CricScore.Controllers.Dto;

public class EmailAvailabilityDto
{
    public EmailAvailabilityDto(bool available)
    {
        Available = available;
    }

    public bool Available { get; set; }
}
