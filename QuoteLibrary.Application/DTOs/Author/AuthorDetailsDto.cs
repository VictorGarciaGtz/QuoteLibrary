using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Author
{
    public class AuthorDetailsDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int IdNationality { get; set; }

        public string? PhotoUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
