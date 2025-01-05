using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.User
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string RoleName { get; set; }

        public required string Email { get; set; }
    }
}
