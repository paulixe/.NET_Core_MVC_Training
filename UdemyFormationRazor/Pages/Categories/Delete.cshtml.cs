using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyFormationRazor.Data;
using UdemyFormationRazor.Models;

namespace UdemyFormationRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void OnGet(int? id)
        {
            if (id == null || id == 0)
            {
                return;
            }
            Category = db.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            Category? category = db.Categories.Find(Category.Id);
            if (category is null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["success"] = "Category correctly deleted  WOWOWOOWOWO!!!";

            return RedirectToPage("Index");
        }
    }
}