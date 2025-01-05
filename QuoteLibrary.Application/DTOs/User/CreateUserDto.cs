using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.User
{
    public class CreateUserDto
    {
        public required string Username { get; set; }

        public required string PasswordHash { get; set; }

        public required string Email { get; set; }
    }
}
