using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Services
{
    public class TypesQuotesService : ITypesQuotesService
    {
        private readonly ITypesQuotesRepository _typeRepository;
        private readonly ITokenService _tokenService;

        public TypesQuotesService(ITypesQuotesRepository typeRepository, ITokenService tokenService)
        {
            _typeRepository = typeRepository;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<TypesQuotesDto>> GetAllTypesQuotesAsync()
        {
            var typesQuotes = await _typeRepository.GetAllTypesQuotesAsync();

            return typesQuotes.Select(t => new TypesQuotesDto { 
                Id = t.Id, 
                Name = t.Name,
                CreationDate = t.CreationDate,
                ModificationDate = t.ModificationDate
            });
        }

        public async Task<TypesQuotesDto?> GetTypesQuotesByIdAsync(int id)
        {
            var typesQuotes = await _typeRepository.GetTypesQuotesByIdAsync(id);

            return typesQuotes == null ? null : new TypesQuotesDto { 
                Id = typesQuotes.Id, 
                Name = typesQuotes.Name, 
                CreationDate = typesQuotes.CreationDate, 
                ModificationDate = typesQuotes.ModificationDate 
            };
        }

        public async Task<int> CreateTypesQuotesAsync(string name)
        {
            var userId = _tokenService.GetUserId();
            var type = new TypesQuotes
            {
                Name = name,
                UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId)    
            };
            return await _typeRepository.CreateTypesQuotesAsync(type);
        }

        public async Task<bool> UpdateTypesQuotesAsync(int id, string name)
        {
            var existingType = await _typeRepository.GetTypesQuotesByIdAsync(id);
            if (existingType == null)
            {
                return false;
            }

            existingType.Name = name;
            return await _typeRepository.UpdateTypesQuotesAsync(existingType);
        }

        public async Task<bool> DeleteTypesQuotesAsync(int id)
        {
            return await _typeRepository.DeleteTypesQuotesAsync(id);
        }
    }
}
