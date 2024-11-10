using QuoteLibrary.Application.DTOs;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;

namespace QuoteLibrary.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) 
        { 
            _usersRepository = usersRepository;
        }

        public async Task<int> CreateUserAsync(UsersInsertDto usersDto)
        {
            var existUser = await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);

            if(existUser) { return 0; }

            var user = new Users
            {
                PasswordHash = usersDto.PasswordHash,
                Id = usersDto.Id,
                RoleName = "User",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                Username = usersDto.Username,
                Email = usersDto.Email
            };

            return await _usersRepository.CreateUsersAsync(user);
        }

        public async Task<bool> DeleteUsersAsync(int id)
        {
            return await _usersRepository.DeleteUsersAsync(id);
        }

        public async Task<bool> ExistUserWithUsernameOrEmail(UsersInsertDto usersDto)
        {
            return await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);
        }

        public async Task<IEnumerable<UsersDto>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
           
            return users != null ? users.Select(x => new UsersDto() { 
                Id = x.Id, 
                CreationDate = x.CreationDate, 
                Email = x.Email, 
                ModificationDate = x.ModificationDate, 
                RoleName = x.RoleName,
                Username = x.Username
            }).ToList() : Enumerable.Empty<UsersDto>();
        }

        public async Task<UsersDto?> GetUsersByIdAsync(int id)
        {
            var user = await _usersRepository.GetUsersByIdAsync(id);

            if (user == null) return null;

            var userDto = new UsersDto()
            {
                Id = user.Id,
                Username = user.Username,
                RoleName = user.RoleName,
                CreationDate = user.CreationDate,
                ModificationDate = user.ModificationDate,
                Email = user.Email,
            };
            return userDto;
        }

        public async Task<bool> UpdateUsersAsync(int id, UsersUpdateDto userDto)
        {
            var existUser = _usersRepository.GetUsersByIdAsync(id);

            if(existUser == null) return false;

            var existUserWithUsernameOrEmail = await _usersRepository.ExistUserWithUsernameOrEmail(userDto.Username, userDto.Email);

            if (existUserWithUsernameOrEmail) { return false; }

            var user = new Users()
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                RoleName = "User"
            };

            return await _usersRepository.UpdateUsersAsync(user);
        }
    }
}
