using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IUsersService
    {
        Task<bool> ExistUserWithUsernameOrEmail(UsersInsertDto usersDto);
        Task<int> CreateUserAsync(UsersInsertDto usersDto);
        Task<UsersDto?> GetUsersByIdAsync(int id);
        Task<bool> UpdateUsersAsync(int id, UsersUpdateDto user);
        Task<bool> DeleteUsersAsync(int id);
        Task<IEnumerable<UsersDto>> GetAllUsersAsync();
    }
}
