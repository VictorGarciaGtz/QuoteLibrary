using QuoteLibrary.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IUsersService
    {
        Task<bool> ExistUserWithUsernameOrEmail(UsersDto usersDto);
        Task<int> CreateUserAsync(UsersDto usersDto);

    }
}
