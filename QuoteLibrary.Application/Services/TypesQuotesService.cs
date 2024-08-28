using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Services
{
    public class TypesQuotesService : ITypesQuotesService
    {
        private readonly ITypesQuotesRepository _typeRepository;

        public TypesQuotesService(ITypesQuotesRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<IEnumerable<TypesQuotes>> GetAllTypesQuotesAsync()
        {
            return await _typeRepository.GetAllTypesQuotesAsync();
        }

        public async Task<TypesQuotes?> GetTypeQuotesByIdAsync(int id)
        {
            return await _typeRepository.GetTypeQuotesByIdAsync(id);
        }
    }
}
