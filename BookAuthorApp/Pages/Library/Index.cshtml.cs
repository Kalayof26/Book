using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookAuthorApp.Pages.Library
{
    public class IndexModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public IndexModel(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public List<Book> Books { get; set; } = new();
        public SelectList Authors { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int? AuthorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        public async Task OnGetAsync()
        {
            // Завжди формуємо SelectList
            var authors = await _authorService.GetAllAsync();
            Authors = new SelectList(authors, "Id", "LastName", AuthorId);

            // Фільтруємо книги
            Books = await _bookService.FilterAsync(AuthorId, Title);
        }

        public IActionResult OnGetReset()
        {
            // Скидаємо фільтри
            return RedirectToPage("/Library/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}


