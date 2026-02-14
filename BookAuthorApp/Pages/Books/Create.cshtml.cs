using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public SelectList AuthorsSelect { get; set; } = null!;

        public async Task OnGetAsync()
        {
            var authors = await _authorService.GetAllAsync();
            AuthorsSelect = new SelectList(
                authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
                "Id",
                "FullName"
            );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Повернути список авторів для повторного рендеру форми
                var authors = await _authorService.GetAllAsync();
                AuthorsSelect = new SelectList(
                    authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
                    "Id",
                    "FullName",
                    Book.AuthorId
                );
                return Page();
            }

            // Додаткова валідація року публікації
            if (Book.PublicationYear < 1450 || Book.PublicationYear > DateTime.Now.Year)
            {
                ModelState.AddModelError("Book.PublicationYear", $"Year must be between 1450 and {DateTime.Now.Year}");
                var authors = await _authorService.GetAllAsync();
                AuthorsSelect = new SelectList(
                    authors.Select(a => new { a.Id, FullName = $"{a.FirstName} {a.LastName}" }),
                    "Id",
                    "FullName",
                    Book.AuthorId
                );
                return Page();
            }

            // Додати книгу
            await _bookService.AddAsync(Book);

            // Повернути на Library
            return RedirectToPage("/Library/Index");
        }
    }
}
