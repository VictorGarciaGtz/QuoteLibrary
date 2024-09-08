using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<IEnumerable<Authors>> GetAllAuthorsAsync();
        Task<Authors?> GetAuthorsByIdAsync(int id);
        Task<int> CreateAuthorsAsync(Authors author);
        Task<bool> UpdateAuthorsAsync(Authors author);
        Task<bool> DeleteAuthorsAsync(int id);
    }
}
