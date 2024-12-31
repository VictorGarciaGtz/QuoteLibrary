﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.DTOs.Quote
{
    public class QuoteDetailsDto
    {
        public int Id { get; set; }

        public required string Text { get; set; }

        public int? AuthorId { get; set; }

        public int TypeId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
