using BookAuthorApp.Data;
using BookAuthorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAuthorApp.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            var existing = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == author.Id);

            if (existing == null) return;

            existing.FirstName = author.FirstName;
            existing.LastName = author.LastName;
            existing.BirthDate = author.BirthDate;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return false;
            if (author.Books.Any()) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
