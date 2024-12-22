﻿using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
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

        public async Task<int> CreateQuotesAsync(QuotesDto quoteDto)
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

        public async Task<IEnumerable<QuotesDto>> GetAllQuotesAsync()
        {
            var quotes = await _quotesRepository.GetAllQuotesAsync();

            return quotes.Select(x => new QuotesDto {
                Id = x.Id,
                Text = x.Text, 
                AuthorId = x.AuthorId, 
                TypeId = x.TypeId, 
                CreationDate = x.CreationDate, 
                ModificationDate = x.ModificationDate 
            });
        }

        public async Task<QuotesDto?> GetQuotesByIdAsync(int id)
        {
            var quote = await _quotesRepository.GetQuotesByIdAsync(id);
            return quote == null ? null : new QuotesDto
            {
                Id = quote.Id,
                Text = quote.Text,
                AuthorId = quote.AuthorId,
                TypeId = quote.TypeId,
                ModificationDate = quote.ModificationDate,
                CreationDate = quote.CreationDate,
            };
        }

        public async Task<bool> UpdateQuotesAsync(int id, QuotesDto quoteDto)
        {
            var quote = new Quotes
            {
                Text = quoteDto.Text,
                AuthorId = quoteDto.AuthorId,
                TypeId = quoteDto.TypeId,
                CreationDate = DateTime.Now,
                ModificationDate = null
            };
            return await _quotesRepository.UpdateQuotesAsync(quote);
        }
    }
}
