using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs
{
    public class RegisterUserDto
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
