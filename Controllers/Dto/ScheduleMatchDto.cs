using System.ComponentModel.DataAnnotations;

namespace CricScore.Controllers.Dto;

public class ScheduleMatchDto
{
    [Required]
    public int HomeTeamId { get; set; }

    [Required]
    public int AwayTeamId { get; set; }

    [Required]
    public DateTime ScheduledDateTime { get; set; }
}
