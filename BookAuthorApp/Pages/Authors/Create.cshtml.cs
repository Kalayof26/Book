using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly AuthorService _service;

        public CreateModel(AuthorService service)
        {
            _service = service;
        }

        [BindProperty]
        public Author Author { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _service.AddAsync(Author);
            return RedirectToPage("Index");
        }
    }
}
