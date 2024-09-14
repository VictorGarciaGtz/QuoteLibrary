
namespace QuoteLibrary.Application.DTOs
{
    public class AuthorsDto
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdNationality { get; set; }
        public string? PhotoUrl { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
