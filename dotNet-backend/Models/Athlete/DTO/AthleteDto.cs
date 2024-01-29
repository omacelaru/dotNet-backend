namespace dotNet_backend.Models.Athlete.DTO;

public abstract class AthleteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
}
public class AthleteCoachNameResponseDto : AthleteDto
{ 
    public string CoachName { get; set; }
}
public class AthleteUsernameResponseDto : AthleteDto
{
    public string Username { get; set; }
}