namespace CricScore.Models;

public class Match : BaseModel
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public int? WinningTeamId { get; set; }
    public int UserId { get; set; }

    public virtual Team? HomeTeam { get; set; }
    public virtual Team? AwayTeam { get; set; }
    public virtual Team? WinningTeam { get; set; }
    public virtual User? User { get; set; }
}
