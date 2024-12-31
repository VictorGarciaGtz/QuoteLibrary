using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.TypesQuotes
{
    public class TypesQuotesDetailsDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
