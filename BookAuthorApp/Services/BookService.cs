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

        // Повертає всі книги з авторами
        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        // Фільтрує книги по AuthorId і Title
        public async Task<List<Book>> FilterAsync(int? authorId, string? title)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author);

            if (authorId.HasValue)
                query = query.Where(b => b.AuthorId == authorId.Value);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            return await query.ToListAsync();
        }

        // Повертає книгу за Id включно з автором
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
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook == null)
                return;

            existingBook.Title = book.Title;
            existingBook.PublicationYear = book.PublicationYear;
            existingBook.Price = book.Price;
            existingBook.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
