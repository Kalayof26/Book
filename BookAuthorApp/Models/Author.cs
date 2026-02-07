using System.ComponentModel.DataAnnotations;

namespace BookAuthorApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
