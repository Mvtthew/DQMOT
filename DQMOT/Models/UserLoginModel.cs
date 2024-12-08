namespace DQMOT.Models;

public record UserLoginModel
{
    public required string Username { get; set; }
    
    public required string Password { get; set; }
}