using QuoteLibrary.Application.DTOs.User;
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
        Task<bool> ExistUserWithUsernameOrEmail(CreateUserDto usersDto);
        Task<int> CreateUserAsync(CreateUserDto usersDto);
        Task<UserDetailsDto?> GetUsersByIdAsync(int id);
        Task<bool> UpdateUsersAsync(int id, UpdateUserDto user);
        Task<bool> DeleteUsersAsync(int id);
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
    }
}
