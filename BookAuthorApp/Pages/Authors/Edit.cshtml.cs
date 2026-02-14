using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly AuthorService _service;

        public EditModel(AuthorService service)
        {
            _service = service;
        }

        [BindProperty]
        public Author Author { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return RedirectToPage("Index");

            Author = existing;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _service.UpdateAsync(Author);
            return RedirectToPage("Index");
        }
    }
}

