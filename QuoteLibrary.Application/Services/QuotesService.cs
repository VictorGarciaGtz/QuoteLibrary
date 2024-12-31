using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.DTOs.Quote;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly ITokenService _tokenService;

        public QuotesService(IQuotesRepository quotesRepository, ITokenService tokenService)
        {
            _quotesRepository = quotesRepository;
            _tokenService = tokenService;
        }

        public async Task<int> CreateQuotesAsync(CreateQuoteDto quoteDto)
        {
            var userId = _tokenService.GetUserId();
            var quote = new Quotes
            {
                Text = quoteDto.Text,
                AuthorId = quoteDto.AuthorId,
                TypeId = quoteDto.TypeId,
                CreationDate = DateTime.Now,
                ModificationDate = null,
                UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId)
            };
            return await _quotesRepository.CreateQuotesAsync(quote);
        }

        public async Task<bool> DeleteQuotesAsync(int id)
        {
            return await _quotesRepository.DeleteQuotesAsync(id);
        }

        public async Task<IEnumerable<GetQuoteDto>> GetAllQuotesAsync()
        {
            var quotes = await _quotesRepository.GetAllQuotesAsync();

            return quotes.Select(x => new GetQuoteDto {
                Id = x.Id ?? 0,
                Text = x.Text, 
                AuthorId = x.AuthorId, 
                TypeId = x.TypeId
            });
        }

        public async Task<QuoteDetailsDto?> GetQuotesByIdAsync(int id)
        {
            var quote = await _quotesRepository.GetQuotesByIdAsync(id);
            return quote == null ? null : new QuoteDetailsDto
            {
                Id = quote.Id ?? 0,
                Text = quote.Text,
                AuthorId = quote.AuthorId,
                TypeId = quote.TypeId,
                ModificationDate = quote.ModificationDate,
                CreationDate = quote.CreationDate,
            };
        }

        public async Task<bool> UpdateQuotesAsync(int id, UpdateQuoteDto quoteDto)
        {
            var quote = await _quotesRepository.GetQuotesByIdAsync(id == 0 ? quoteDto.Id : id);
            if (quote == null)
            {
                return false;
            }
            var userId = _tokenService.GetUserId();

            quote.Text = quoteDto.Text;
            quote.AuthorId = quoteDto.AuthorId;
            quote.TypeId = quote.TypeId;
            quote.UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId);

            return await _quotesRepository.UpdateQuotesAsync(quote);
        }
    }
}
