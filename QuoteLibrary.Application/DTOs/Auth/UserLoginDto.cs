using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Auth
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 300 characters")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 50 characters")]
        public required string Password { get; set; }
    }
}
