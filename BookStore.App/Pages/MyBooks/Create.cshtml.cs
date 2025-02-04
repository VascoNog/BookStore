using BookStore.App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;




namespace BookStore.App.Pages.MyBooks;

[Authorize] // Coloca-se isto para permitir o acesso apenas a utilizadores autenticados

public class CreateModel : PageModel
{

    [BindProperty]
    public Book Book { get; set; }


    private readonly BookDbContext _context;

    public CreateModel(BookDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _context.Books.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
