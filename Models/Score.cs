using System.ComponentModel.DataAnnotations;

namespace CricScore.Models;

public class Score : BaseModel
{
    [Required]
    public int MatchId { get; set; }

    [Required]
    public int Innings { get; set; }

    [Required]
    public int BattingTeamId { get; set; }

    [Required]
    public int Runs { get; set; }

    [Required]
    public int Wickets { get; set; }

    [Required]
    public int Overs { get; set; }

    [Required]
    public int Balls { get; set; }

    [Required]
    public int MaxOvers { get; set; }

    public virtual Match? Match { get; set; }
    public virtual Team? BattingTeam { get; set; }
}
