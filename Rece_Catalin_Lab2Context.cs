using Microsoft.EntityFrameworkCore;

public class Rece_Catalin_Lab2Context(DbContextOptions<Rece_Catalin_Lab2Context> options) : DbContext(options)
{
    public DbSet<Rece_Catalin_Lab2.Models.Book> Book { get; set; } = default!;
}
