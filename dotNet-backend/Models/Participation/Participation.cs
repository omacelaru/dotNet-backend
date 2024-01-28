using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Participation;

public class Participation 
{
    public Guid AthleteId { get; set; }
    public Athlete.Athlete Athlete { get; set; }
    public Guid CompetitionId { get; set; }
    public Competition.Competition Competition { get; set; }
    
    public int? FirstPlace { get; set; }
    public int? SecondPlace { get; set; }
    public int? ThirdPlace { get; set; }
}