using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rece_Catalin_Lab2.Models;
using Rece_Catalin_Lab2.Models.ViewModels;

namespace Rece_Catalin_Lab2.Pages.CategoryPages
{
    public class IndexModel : PageModel
    {
        private readonly Rece_Catalin_Lab2Context _context;

        public IndexModel(Rece_Catalin_Lab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; } = default!;
        public int CategoryID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();

            // Încărcăm categoriile împreună cu cărțile și autorii lor
            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .Single();

                // Extragem cărțile asociate categoriei selectate
                CategoryData.Books = category.BookCategories.Select(s => s.Book);
            }
        }
    }
}
