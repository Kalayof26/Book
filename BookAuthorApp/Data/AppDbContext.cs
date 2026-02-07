using BookAuthorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAuthorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
    }
}
