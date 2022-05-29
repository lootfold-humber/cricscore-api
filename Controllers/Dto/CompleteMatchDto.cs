using System.ComponentModel.DataAnnotations;

namespace CricScore.Controllers.Dto;

public class CompleteMatchDto
{
    [Required]
    public int WinningTeamId { get; set; }
}
