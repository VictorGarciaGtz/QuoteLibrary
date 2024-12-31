using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Quote
{
    public class UpdateQuoteDto
    {
        public int Id { get; set; }

        public required string Text { get; set; }

        public int? AuthorId { get; set; }

        public int TypeId { get; set; }
    }
}
