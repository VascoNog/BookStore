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
    private readonly BookRepository _bookRepository;

    public DetailsModel(BookRepository bookRepository)
    {
       _bookRepository = bookRepository;
    }
    public Book Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Book = await _bookRepository.GetBookByIdAsync(id.Value);
        return Book is null ? NotFound() : Page();
    }
}
