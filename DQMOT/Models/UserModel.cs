namespace DQMOT.Models;

public record UserModel
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}