using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookService _service;

        public IndexModel(BookService service)
        {
            _service = service;
        }

        public List<Book> Books { get; set; } = new();

        public async Task OnGetAsync()
        {
            Books = await _service.GetAllAsync();
        }
    }
}

