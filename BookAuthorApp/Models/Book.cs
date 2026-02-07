using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAuthorApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required] public string Title { get; set; } = string.Empty;
        [Range(1450, 2100)] public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        [Required] public string ISBN { get; set; } = string.Empty;

        [ForeignKey("Author")] public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
