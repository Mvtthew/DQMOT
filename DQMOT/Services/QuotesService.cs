using DQMOT.Data;
using DQMOT.Entities;
using DQMOT.Models;
using Microsoft.EntityFrameworkCore;

namespace DQMOT.Services;

public class QuotesService : IQuotesService
{
    private readonly DataContext _dataContext;
    
    public QuotesService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<QuoteModel>> GetQuotesAsync(CancellationToken cancellationToken = default)
    {
        return await _dataContext.Quotes
            .Include(q => q.Creator)
            .Select(q => q.toModel())
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> IsUserQuoteCreatorAsync(
        int quoteId, 
        User user, 
        CancellationToken cancellationToken = default)
    {
        return false;
    }
    
    public async Task<QuoteModel> CreateQuoteAsync(
        QuoteCreateModel quoteCreateModel, 
        User creator,
        CancellationToken cancellationToken = default)
    {
        var quote = new Quote
        {
            QuoteText = quoteCreateModel.QuoteText,
            QuoteAuthor = quoteCreateModel.QuoteAuthor,
            QuoteDate = quoteCreateModel.QuoteDate,
            Creator = creator
        };
        
        _dataContext.Quotes.Add(quote);
        await _dataContext.SaveChangesAsync(cancellationToken);
        
        return quote.toModel();
    }
    
    public async Task<QuoteModel> EditQuoteAsync(
        int id,
        QuoteCreateModel quoteCreateModel, 
        CancellationToken cancellationToken = default)
    {
        var quote = await _dataContext.Quotes.FindAsync(id);
        
        if (quote == null)
        {
            throw new Exception("Quote not found");
        }
        
        quote.QuoteText = quoteCreateModel.QuoteText;
        quote.QuoteAuthor = quoteCreateModel.QuoteAuthor;
        quote.QuoteDate = quoteCreateModel.QuoteDate;
        quote.UpdatedAt = DateTime.Now;
        
        await _dataContext.SaveChangesAsync(cancellationToken);
        
        return quote.toModel();
    }
    
    public async Task DeleteQuoteAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var quote = await _dataContext.Quotes.FindAsync(id);
        
        if (quote == null)
        {
            throw new Exception("Quote not found");
        }
        
        _dataContext.Quotes.Remove(quote);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}