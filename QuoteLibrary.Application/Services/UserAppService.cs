using QuoteLibrary.Application.Interfaces;
using QuoteLibrary.Domain.Interfaces;
using QuoteLibrary.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserAppService(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            return await _userService.RegisterUserAsync(username, password);
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userService.ValidateUserAsync(username, password);

            if (user == null)
                return null;

            return _jwtTokenService.GenerateJwtToken(username, user.RoleName);
        }
    }
}
