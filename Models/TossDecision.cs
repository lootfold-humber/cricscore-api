using System.ComponentModel.DataAnnotations;

namespace CricScore.Models;

public class TossDecision : BaseModel
{
    [Required]
    [StringLength(5)]
    public string? Value { get; set; }
}
