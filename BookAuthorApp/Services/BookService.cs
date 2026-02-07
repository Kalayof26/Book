using BookAuthorApp.Data;
using BookAuthorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAuthorApp.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            var existing = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existing == null) return;

            existing.Title = book.Title;
            existing.PublicationYear = book.PublicationYear;
            existing.Price = book.Price;
            existing.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}

