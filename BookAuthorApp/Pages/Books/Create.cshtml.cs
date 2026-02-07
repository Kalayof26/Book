using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        public CreateModel(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public SelectList AuthorsSelect { get; set; } = new SelectList(new List<Author>(), "Id", "FirstName");

        public async Task OnGetAsync()
        {
            var authors = await _authorService.GetAllAsync();
            AuthorsSelect = new SelectList(authors, "Id", "FirstName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Book.PublicationYear < 1450 || Book.PublicationYear > DateTime.Now.Year)
            {
                ModelState.AddModelError("Book.PublicationYear", "Year must be between 1450 and current year");
                var authors = await _authorService.GetAllAsync();
                AuthorsSelect = new SelectList(authors, "Id", "FirstName", Book.AuthorId);
                return Page();
            }

            await _bookService.AddAsync(Book);
            return RedirectToPage("Index");
        }
    }
}
