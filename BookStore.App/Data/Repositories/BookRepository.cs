using BookStore.App.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App;

public class BookRepository
{

    private readonly BookDbContext _ctx;

    public BookRepository(BookDbContext ctx)
    {
        _ctx = ctx;
    }

    public Book GetBookById(int id)
    {
        return _ctx.Books.FirstOrDefault(b => b.Id == id);
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _ctx.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

}
