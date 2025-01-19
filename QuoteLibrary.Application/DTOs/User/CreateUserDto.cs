using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 300 characters")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 50 characters")]
        public required string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Email must be between 5 and 100 characters")]
        public required string Email { get; set; }
    }
}
