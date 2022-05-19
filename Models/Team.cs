using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CricScore.Models;

public class Team : BaseModel
{
    [Required]
    [StringLength(255)]
    public string? Name { get; set; }

    public int UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Match>? HomeMatches { get; set; }
    public virtual ICollection<Match>? AwayMatches { get; set; }
    public virtual ICollection<Match>? MatchesWon { get; set; }
}
