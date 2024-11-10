using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();

        Task<bool> ExistUserWithUsernameOrEmail(string username, string email);

        Task<Users?> GetUsersByIdAsync(int id);
        Task<int> CreateUsersAsync(Users user);
        Task<bool> UpdateUsersAsync(Users user);
        Task<bool> DeleteUsersAsync(int id);
    }
}
