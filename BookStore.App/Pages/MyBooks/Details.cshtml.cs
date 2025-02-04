using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.App.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.App.Pages.MyBooks;

[Authorize] // Coloca-se isto para permitir o acesso apenas a utilizadores autenticados
public class DetailsModel : PageModel
{
    private readonly BookStore.App.Data.BookDbContext _context;

    public DetailsModel(BookStore.App.Data.BookDbContext context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

        if (book is not null)
        {
            Book = book;

            return Page();
        }

        return NotFound();
    }
}
