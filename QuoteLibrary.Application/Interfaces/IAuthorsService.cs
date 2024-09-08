using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Domain.Entities;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorsDto>> GetAllAuthorsAsync();
        Task<AuthorsDto?> GetAuthorsByIdAsync(int id);
        Task<int> CreateAuthorsAsync(AuthorsDto authorDto);
        Task<bool> UpdateAuthorsAsync(AuthorsDto authorDto);
        Task<bool> DeleteAuthorsAsync(int id);
    }
}
