using Microsoft.AspNetCore.Http;
using QuoteLibrary.Application.DTOs.User;
using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Entities;
using QuoteLibrary.Domain.Interfaces;
using System.Security.Claims;

namespace QuoteLibrary.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;

        public UsersService(IUsersRepository usersRepository, ITokenService tokenService) 
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }

        public async Task<int> CreateUserAsync(CreateUserDto usersDto)
        {
            var existUser = await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);

            if(existUser) { return 0; }

            var user = new Users
            {
                PasswordHash = usersDto.PasswordHash,
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
            if (!IsValidClaimsUserByRole(id))
                throw new Exception(string.Format("This user is not authorized to perform this action to the element with Id {0}.", id));

            return await _usersRepository.DeleteUsersAsync(id);
        }

        public async Task<bool> ExistUserWithUsernameOrEmail(CreateUserDto usersDto)
        {
            return await _usersRepository.ExistUserWithUsernameOrEmail(usersDto.Username, usersDto.Email);
        }

        public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
           
            return users != null ? users.Select(x => new GetUserDto() { 
                Id = x.Id, 
                Email = x.Email, 
                RoleName = x.RoleName,
                Username = x.Username
            }).ToList() : Enumerable.Empty<GetUserDto>();
        }

        public async Task<UserDetailsDto?> GetUsersByIdAsync(int id)
        {
            if (!IsValidClaimsUserByRole(id))
                throw new Exception(string.Format("This user is not authorized to perform this action to the element with Id {0}.", id));

            var user = await _usersRepository.GetUsersByIdAsync(id);

            if (user == null) return null;

            var userDto = new UserDetailsDto()
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

        public async Task<bool> UpdateUsersAsync(int id, UpdateUserDto userDto)
        {
            if (!IsValidClaimsUserByRole(id))
                throw new Exception(string.Format("This user is not authorized to perform this action to the element with Id {0}.", id));

            var existUser = await _usersRepository.GetUsersByIdAsync(id);

            if(existUser == null) return false;

            if(!(existUser.Email == userDto.Email && existUser.Username == userDto.Username)) {
                if (existUser.Username == userDto.Username)
                {
                    var existUserWithSameEmail = await _usersRepository.ExistsOtherUserWithSameEmail(id, userDto.Email);
                    if (existUserWithSameEmail) { return false; }
                } else if(existUser.Email == userDto.Email)
                {
                    var existUserWithSameUsername = await _usersRepository.ExistsOtherUserWithSameUsername(id, userDto.Username);
                    if (existUserWithSameUsername) { return false; }
                } else
                {
                    var existsUserWithUsernameOrEmail = await _usersRepository.ExistUserWithUsernameOrEmail(userDto.Username, userDto.Email);
                    if (existsUserWithUsernameOrEmail) { return false; }
                }
            }

            var user = new Users()
            {
                Id = 0,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                RoleName = "User"
            };

            return await _usersRepository.UpdateUsersAsync(user);
        }

        private bool IsValidClaimsUserByRole(int id)
        {
            var authenticatedUserId = _tokenService.GetUserId();

            var userId = 0;

            if (!int.TryParse(authenticatedUserId, out userId))
                return false;

            var userRole = _tokenService.GetUserRole();

            if(userRole == "Admin")
                return true;
            else if (userRole == "User" && id == userId)
                return true;

            return false;
        }
    }  
}
