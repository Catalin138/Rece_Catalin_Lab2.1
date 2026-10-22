using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rece_Catalin_Lab2.Models;

namespace Rece_Catalin_Lab2.Pages.BookPages
{
    public class DetailsModel : PageModel
    {
        private readonly Rece_Catalin_Lab2Context _context;

        public DetailsModel(Rece_Catalin_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            // Includem Publisher și BookCategories împreună cu Category
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
