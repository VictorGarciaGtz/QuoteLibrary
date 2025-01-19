using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Quote
{
    public class UpdateQuoteDto
    {
        [Required(ErrorMessage = "Id is required")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1000 characters")]
        public required string Text { get; set; }

        public int? AuthorId { get; set; }

        [Required(ErrorMessage = "TypeId is required")]
        public required int TypeId { get; set; }
    }
}
