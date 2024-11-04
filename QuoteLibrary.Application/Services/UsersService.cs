using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) 
        { 
            _usersRepository = usersRepository;
        }
        public async Task<int> CreateUserAsync(UsersDto usersDto)
        {
            var existUser = await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);

            if(existUser) { return 0; }

            var user = new Users
            {
                PasswordHash = usersDto.PasswordHash,
                Id = usersDto.Id,
                RoleName = usersDto.RoleName,
                CreationDate = usersDto.CreationDate,
                ModificationDate = usersDto.ModificationDate,
                Username = usersDto.Username,
                Email = usersDto.Email
            };

            return await _usersRepository.CreateUsersAsync(user);
        }

        public async Task<bool> ExistUserWithUsernameOrEmail(UsersDto usersDto)
        {
            return await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);
        }
    }
}
