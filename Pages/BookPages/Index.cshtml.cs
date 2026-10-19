using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rece_Catalin_Lab2.Models;

namespace Rece_Catalin_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Rece_Catalin_Lab2Context _context;

    public IndexModel(Rece_Catalin_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Book = await _context.Book
            .Include(b => b.Publisher)
            .ToListAsync();
    }
}
