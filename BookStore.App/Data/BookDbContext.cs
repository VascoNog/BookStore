using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Data;

public class BookDbContext : IdentityDbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }


    public DbSet<Book> Books {  get; set; }
}



