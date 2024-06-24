using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyFormation.Models;

namespace UdemyFormation.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var categoryA = new Category() { Id = 1, Name = "Action", DisplayOrder = 1 };
            var categoryB = new Category() { Id = 2, Name = "Scifi", DisplayOrder = 2 };
            var categoryC = new Category() { Id = 3, Name = "History", DisplayOrder = 3 };
            modelBuilder.Entity<Category>().HasData(categoryA, categoryB, categoryC);

            modelBuilder.Entity<Product>().HasData(
                new Product() { CategoryId = categoryA.Id, Id = 1, Title = "Save the soldier", Price = 10f, Price10 = 9f, Description = "A young nurse has to save a soldier from death." },
                new Product() { CategoryId = categoryB.Id, Id = 2, Title = "Explosion", Price = 11.60f, Price10 = 10f, Description = "Special effects everywhere. Bring solar glasses if you don't to become blind." },
                new Product() { CategoryId = categoryC.Id, Id = 3, Title = "Job affair", Price = 9f, Price10 = 8f, Description = "Work or die. That's the sentence said everyday in the company of Bob." },
                new Product() { CategoryId = categoryA.Id, Id = 4, Title = "Love story", Price = 10f, Price10 = 9.50f, Description = "Love must be fed everyday or it perished into a disaster." }
                );
        }
    }
}