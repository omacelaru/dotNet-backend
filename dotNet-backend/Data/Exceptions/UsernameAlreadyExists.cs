namespace dotNet_backend.Data.Exceptions;

public class UsernameAlreadyExists : Exception
{
    public UsernameAlreadyExists() : base("Username already exists") {}
    
    public UsernameAlreadyExists(string message) : base(message) {}
    
}