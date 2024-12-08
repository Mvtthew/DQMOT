using DQMOT.Models;

namespace DQMOT.Entities;

public record Quote
{
    public int Id { get; set; }
    
    public required User Creator { get; set; }
    
    public required string QuoteText { get; set; }
    
    public required string QuoteAuthor { get; set; }
    
    public required DateTime QuoteDate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public QuoteModel toModel()
    {
        return new QuoteModel()
        {
            Id = Id,
            Creator = Creator.toModel(),
            QuoteText = QuoteText,
            QuoteAuthor = QuoteAuthor,
            QuoteDate = QuoteDate,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }
}