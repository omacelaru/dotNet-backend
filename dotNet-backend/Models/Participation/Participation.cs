namespace dotNet_backend.Models.Participation;

public class Participation 
{
    public Guid AthleteId { get; set; }
    public Athlete.Athlete Athlete { get; set; }
    public Guid CompetitionId { get; set; }
    public Competition.Competition Competition { get; set; }

    public int FirstPlace { get; set; } = 0;
    public int SecondPlace { get; set; } = 0;
    public int ThirdPlace { get; set; } = 0;
}