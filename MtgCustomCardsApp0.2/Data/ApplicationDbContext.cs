using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsApp0._2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Card> CardData { get; set; }

    public DbSet<ImageData> ImageData { get; set; }
}