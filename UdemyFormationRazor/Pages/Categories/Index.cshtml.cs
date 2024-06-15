using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyFormationRazor.Models;
using UdemyFormationRazor.Data;

namespace UdemyFormationRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public List<Category> Categories { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void OnGet()
        {
            Categories = db.Categories.ToList();
        }
    }
}