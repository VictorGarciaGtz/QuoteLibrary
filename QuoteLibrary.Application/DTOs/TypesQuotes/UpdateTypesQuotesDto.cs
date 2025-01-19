using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.TypesQuotes
{
    public class UpdateTypesQuotesDto
    {
        [Required(ErrorMessage = "Id is required")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public required string Name { get; set; }
    }
}
