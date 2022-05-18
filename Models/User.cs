using System.ComponentModel.DataAnnotations;

namespace CricScore.Models;

public class User : BaseModel
{
    [Required]
    [StringLength(100)]
    public string? First { get; set; }

    [Required]
    [StringLength(100)]
    public string? Last { get; set; }

    [Required]
    [StringLength(100)]
    public string? Email { get; set; }

    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
}
