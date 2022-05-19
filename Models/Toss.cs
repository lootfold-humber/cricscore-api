namespace CricScore.Models;

public class Toss : BaseModel
{
    public int MatchId { get; set; }
    public int WinningTeamId { get; set; }
    public int TossDecisionId { get; set; }

    public virtual Team? WinningTeam { get; set; }
    public virtual TossDecision? TossDecision { get; set; }
    public virtual Match? Match { get; set; }
}
