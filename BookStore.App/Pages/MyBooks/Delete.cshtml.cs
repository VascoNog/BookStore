using BookStore.App.Data;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BookStore.App.Pages.MyBooks;

[Authorize] // Coloca-se isto para permitir o acesso apenas a utilizadores autenticados
public class DeleteModel : PageModel
{
    private readonly BookRepository _bookRepository;

    public DeleteModel(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Book = await _bookRepository.GetBookByIdAsync(id.Value);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Result result = await _bookRepository.TryDeleteBookAsync(Book, userId);
        return RedirectToPage("./Index");
    }
}