using CricScore.Models;

namespace CricScore.Controllers.Dto;

public class GetScoreDto
{
    public Score? FirstInnings { get; set; }
    public Score? SecondInnings { get; set; }
}
