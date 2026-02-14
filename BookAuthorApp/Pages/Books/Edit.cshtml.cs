using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace BookAuthorApp.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public EditModel(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public SelectList AuthorsSelect { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var existing = await _bookService.GetByIdAsync(id);
            if (existing == null)
                return RedirectToPage("/Library/Index");

            Book = existing;

            var authors = await _authorService.GetAllAsync();
            AuthorsSelect = new SelectList(
                authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
                "Id",
                "FullName",
                Book.AuthorId
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var authors = await _authorService.GetAllAsync();
                AuthorsSelect = new SelectList(
                    authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
                    "Id",
                    "FullName",
                    Book.AuthorId
                );
                return Page();
            }

            await _bookService.UpdateAsync(Book);
            return RedirectToPage("/Library/Index");
        }
    }
}

