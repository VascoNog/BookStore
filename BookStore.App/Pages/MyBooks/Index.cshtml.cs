using BookStore.App.Data;
using BookStore.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Pages.MyBooks;

[Authorize] // Coloca-se isto para permitir o acesso apenas a utilizadores autenticados

public partial class IndexModel : PageModel
{
    public IList<BookModel> Books { get; set; }

    private readonly BookRepository _bRep;
    public IndexModel(BookRepository bookRepository)
    {
        _bRep = bookRepository;
    }

    public async Task OnGetAsync()
    {
        Books = await _bRep.GetBooksAsync();
    }

}