using Microsoft.EntityFrameworkCore;
using UdemyFormation.Models;

namespace UdemyFormation.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category() { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category() { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Title = "Save the soldier", Price = 10f, Price10 = 9f, Description = "A young nurse has to save a soldier from death." },
                new Product() { Id = 2, Title = "Explosion", Price = 11.60f, Price10 = 10f, Description = "Special effects everywhere. Bring solar glasses if you don't to become blind." },
                new Product() { Id = 3, Title = "Job affair", Price = 9f, Price10 = 8f, Description = "Work or die. That's the sentence said everyday in the company of Bob." },
                new Product() { Id = 4, Title = "Love story", Price = 10f, Price10 = 9.50f, Description = "Love must be fed everyday or it perished into a disaster." }
                );
        }
    }
}