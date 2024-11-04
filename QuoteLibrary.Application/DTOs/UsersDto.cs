using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs
{
    public class UsersDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string RoleName { get; set; }

        public required string PasswordHash { get; set; }

        public required string Email { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }


    }
}
