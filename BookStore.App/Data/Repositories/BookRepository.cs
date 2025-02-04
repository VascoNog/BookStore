using BookStore.App.Data;
using BookStore.App.Pages.MyBooks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using FluentResults;
using BookStore.App.Models;

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

    public async Task<Result> TrySaveEditionAsync(Book currentBook)
    {
        try
        {
            _ctx.Attach(currentBook).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Error updating book");
        }
    }
       
    public async Task<List<BookModel>> GetBooksAsync()
    {
        return await (from b in _ctx.Books
                      join u in _ctx.Users on b.UserId equals u.Id
                      select new BookModel
                      {
                          Id = b.Id,
                          Title = b.Title,
                          Isbn = b.Isbn,
                          Category = b.Category,
                          UserId = b.UserId,
                          UserEmail = u.Email,
                      }).ToListAsync();
    }

    public async Task<Result> TryDeleteBookAsync(Book currentBook, string userId)
    {
        try
        {
            int res = await _ctx.Books.Where(Book => Book.Id == currentBook.Id && Book.UserId == userId).ExecuteDeleteAsync();
            return res > 0 ? Result.Ok() : Result.Fail("Book not found");

        }
        catch
        {
            return Result.Fail("Error deleting book");
        }
    }

}
