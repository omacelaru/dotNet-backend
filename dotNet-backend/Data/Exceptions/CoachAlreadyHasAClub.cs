namespace dotNet_backend.Data.Exceptions;

public class CoachAlreadyHasAClub : Exception
{
    public CoachAlreadyHasAClub() : base("Coach already has a club") {}
    
    public CoachAlreadyHasAClub(string message) : base(message) {}
    
}