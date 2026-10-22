using Microsoft.EntityFrameworkCore;

public class Rece_Catalin_Lab2Context(DbContextOptions<Rece_Catalin_Lab2Context> options) : DbContext(options)
{
    public DbSet<Rece_Catalin_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Rece_Catalin_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Rece_Catalin_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Rece_Catalin_Lab2.Models.Category> Category { get; set; } = default!;
}
