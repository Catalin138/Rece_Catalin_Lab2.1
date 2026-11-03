using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rece_Catalin_Lab2.Data;
using Rece_Catalin_Lab2.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurare pentru contextul principal al aplicației (Cărți, Autori, etc.)
var connectionString = builder.Configuration.GetConnectionString("Rece_Catalin_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Rece_Catalin_Lab2Context' not found.");
builder.Services.AddDbContext<Rece_Catalin_Lab2Context>(options =>
    options.UseSqlServer(connectionString));

// 2. Configurare pentru contextul de Identity (Autentificare și Utilizatori)
var identityConnectionString = builder.Configuration.GetConnectionString("LibraryIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryIdentityContextConnection' not found.");
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(identityConnectionString));

// Configurare Identity folosind ApplicationUser în loc de IdentityUser
builder.Services.AddDefaultIdentity<Rece_Catalin_Lab2.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // IMPORTANT: Activare autentificare
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
