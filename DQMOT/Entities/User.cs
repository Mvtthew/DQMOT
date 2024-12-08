using DQMOT.Models;

namespace DQMOT.Entities;

public record User
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public UserModel toModel()
    {
        return new UserModel()
        {
            Id = Id,
            Username = Username,
            CreatedAt = CreatedAt
        };
    }
}