using DQMOT.Entities;
using DQMOT.Models;
using DQMOT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DQMOT.Controllers
{
    [Authorize]
    [Route("api/quotes")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;
        private readonly IUsersService _usersService;
        
        public QuotesController(
            IQuotesService quotesService,
            IUsersService usersService)
        {
            _quotesService = quotesService;
            _usersService = usersService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuotes(CancellationToken cancellationToken = default)
        {
            var quotes = await _quotesService.GetQuotesAsync(cancellationToken);
            return Ok(quotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote(
            [FromBody] QuoteCreateModel quoteCreateModel,
            CancellationToken cancellationToken = default)
        {
            var user = await _usersService.GetUserFromPrincipalAsync(User, cancellationToken);
            
            var createdQuote = await _quotesService.CreateQuoteAsync(
                quoteCreateModel, 
                user,
                cancellationToken);
            return Ok(createdQuote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditQuote(
            [FromRoute] int id,
            [FromBody] QuoteCreateModel quoteCreateModel,
            CancellationToken cancellationToken = default)
        {
            var user = await _usersService.GetUserFromPrincipalAsync(User, cancellationToken);
            var isUserQuoteCreator = await _quotesService.IsUserQuoteCreatorAsync(id, user, cancellationToken);
            if (!isUserQuoteCreator)
            {
                return Unauthorized("You are not the creator of this quote");
            }
            
            var editedQuote = await _quotesService.EditQuoteAsync(id, quoteCreateModel, cancellationToken);
            return Ok(editedQuote);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var user = await _usersService.GetUserFromPrincipalAsync(User, cancellationToken);
            var isUserQuoteCreator = await _quotesService.IsUserQuoteCreatorAsync(id, user, cancellationToken);
            if (!isUserQuoteCreator)
            {
                return Unauthorized("You are not the creator of this quote");
            }
            await _quotesService.DeleteQuoteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
