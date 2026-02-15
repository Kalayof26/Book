using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Book Book { get; set; } = null!;

        public List<SelectListItem> AuthorsSelect { get; set; } = new();

        private async Task LoadAuthorsAsync()
        {
            var authors = await _authorService.GetAllAsync();

            AuthorsSelect = authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.FirstName} {a.LastName}",
                Selected = Book != null && a.Id == Book.AuthorId
            }).ToList();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return RedirectToPage("/Books/Index");

            Book = book;
            await LoadAuthorsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAuthorsAsync();
                return Page();
            }

            await _bookService.UpdateAsync(Book);
            return RedirectToPage("/Books/Index");
        }
    }
}


