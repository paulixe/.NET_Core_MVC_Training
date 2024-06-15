using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyFormationRazor.Data;
using UdemyFormationRazor.Models;

namespace UdemyFormationRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name should be different than display order");
            }

            if (ModelState.IsValid)
            {
                db.Categories.Add(Category);
                db.SaveChanges();
                TempData["success"] = "Category correctly created  WOWOWOOWOWO!!!";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}