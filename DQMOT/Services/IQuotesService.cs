using DQMOT.Entities;
using DQMOT.Models;

namespace DQMOT.Services;

public interface IQuotesService
{
    Task<IEnumerable<QuoteModel>> GetQuotesAsync(
        CancellationToken cancellationToken = default);
    
    Task<bool> IsUserQuoteCreatorAsync(
        int quoteId,
        User user,
        CancellationToken cancellationToken = default);
    
    Task<QuoteModel> CreateQuoteAsync(
        QuoteCreateModel quoteCreateModel, 
        User creator,
        CancellationToken cancellationToken = default);

    Task<QuoteModel> EditQuoteAsync(
        int id,
        QuoteCreateModel quoteCreateModel,
        CancellationToken cancellationToken = default);

    Task DeleteQuoteAsync(
        int id,
        CancellationToken cancellationToken = default);
}