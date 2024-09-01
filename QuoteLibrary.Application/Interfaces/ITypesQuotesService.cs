using QuoteLibrary.Application.DTOs;

namespace QuoteLibrary.Application.Interfaces
{
    public interface ITypesQuotesService
    {
        Task<IEnumerable<TypesQuotesDto>> GetAllTypesQuotesAsync();
        Task<TypesQuotesDto?> GetTypesQuotesByIdAsync(int id);

        Task<int> CreateTypesQuotesAsync(string name);
        Task<bool> UpdateTypesQuotesAsync(int id, string name);
        Task<bool> DeleteTypesQuotesAsync(int id);
    }
}
