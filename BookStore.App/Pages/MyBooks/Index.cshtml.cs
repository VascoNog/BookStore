using BookStore.App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Pages.MyBooks;

[Authorize] // Coloca-se isto para permitir o acesso apenas a utilizadores autenticados

public class IndexModel : PageModel
{
    public IList<BookModel> Books { get; set; }

    private readonly BookDbContext _ctx;

    public IndexModel(BookDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task OnGetAsync()
    {

        IList<BookModel> Books = await (from b in _ctx.Books
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

    // DATA TRANSFER OBJECT (DTO) // PASSAR PARA UMA PASTA MODELS
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}