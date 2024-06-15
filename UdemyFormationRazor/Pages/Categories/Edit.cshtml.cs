using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyFormationRazor.Data;
using UdemyFormationRazor.Models;

namespace UdemyFormationRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(Category);
                db.SaveChanges();
                TempData["success"] = "Category correctly updated  WOWOWOOWOWO!!!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}