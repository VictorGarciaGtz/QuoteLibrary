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
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthUserService(IAuthUserRepository userService, IJwtTokenService jwtTokenService)
        {
            _authUserRepository = userService;
            _jwtTokenService = jwtTokenService;
        }
       
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _authUserRepository.ValidateUserAsync(username, password);

            if (user == null)
                return null;

            return _jwtTokenService.GenerateJwtToken(username, user.RoleName);
        }
    }
}
