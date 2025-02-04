using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Data;

public class BookDbContext : IdentityDbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books {  get; set; }
    public DbSet<RentedBook> RentedBooks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RentedBook>()
            .Property(p=> p.RentedAt)
            .HasColumnType("datetime2(0)");

        modelBuilder.Entity<RentedBook>()
            .Property(p => p.ReturnedAt)
            .HasColumnType("datetime2(0)");
    }
}



