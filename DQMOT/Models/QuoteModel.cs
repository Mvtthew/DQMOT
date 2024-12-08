namespace DQMOT.Models;

public record QuoteModel
{
    public int Id { get; set; }
    
    public required UserModel Creator { get; set; }
    
    public required string QuoteText { get; set; }
    
    public required string QuoteAuthor { get; set; }
    
    public required DateTime QuoteDate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}