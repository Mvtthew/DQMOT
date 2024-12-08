namespace DQMOT.Models;

public record QuoteCreateModel
{
    public required string QuoteText { get; set; }
    
    public required string QuoteAuthor { get; set; }
    
    public required DateTime QuoteDate { get; set; }
}