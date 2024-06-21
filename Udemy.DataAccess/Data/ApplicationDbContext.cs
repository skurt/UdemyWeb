using Microsoft.EntityFrameworkCore;
using UdemyWeb.Models.Models;

namespace UdemyWeb.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Category 1", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Sci Fi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Poetry", DisplayOrder = 3 }
            );
    }
}
