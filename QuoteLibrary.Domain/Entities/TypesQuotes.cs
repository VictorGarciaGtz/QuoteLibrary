using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Entities
{
    public class TypesQuotes
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int UserId { get; set; }
    }
}
