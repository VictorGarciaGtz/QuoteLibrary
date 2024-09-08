﻿

namespace QuoteLibrary.Application.DTOs
{
    public class TypesQuotesDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
