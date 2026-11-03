using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rece_Catalin_Lab2.Models;

namespace Rece_Catalin_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Rece_Catalin_Lab2Context _context;

    public EditModel(Rece_Catalin_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        Borrowing = borrowing;

        // Populare liste derulante (Numele membrului și Titlul cărții)
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
        ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title"); // Dacă proprietatea se numește BookTitle în loc de Title, modifică aici

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Re-populare liste în caz de eroare de validare, altfel pagina va da eroare când se întoarce
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName", Borrowing.MemberID);
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", Borrowing.BookID);
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }
}
