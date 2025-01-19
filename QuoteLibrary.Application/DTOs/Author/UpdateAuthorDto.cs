using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Author
{
    public class UpdateAuthorDto
    {
        [Required(ErrorMessage = "Id is required")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 200 characters")]
        public required string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int IdNationality { get; set; }

        public string? PhotoUrl { get; set; }
    }
}
