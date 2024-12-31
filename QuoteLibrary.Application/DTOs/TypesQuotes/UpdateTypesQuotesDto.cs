using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.TypesQuotes
{
    public class UpdateTypesQuotesDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
