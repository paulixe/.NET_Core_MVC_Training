using Microsoft.AspNetCore.Mvc;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfAction unitOfAction;
        public ICategoryRepository CategoryRepository => unitOfAction.Category;

        public CategoryController(IUnitOfAction unitOfAction)
        {
            this.unitOfAction = unitOfAction;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = CategoryRepository.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name should be different than display order");
            }

            if (ModelState.IsValid)
            {
                CategoryRepository.Add(category);
                unitOfAction.Save();
                TempData["success"] = "Category correctly created  WOWOWOOWOWO!!!";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = CategoryRepository.Get(cat => cat.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Update(category);
                unitOfAction.Save();
                TempData["success"] = "Category correctly updated  WOWOWOOWOWO!!!";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = CategoryRepository.Get(cat => cat.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = CategoryRepository.Get(cat => cat.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            CategoryRepository.Remove(category);
            unitOfAction.Save();
            TempData["success"] = "Category correctly deleted  WOWOWOOWOWO!!!";

            return RedirectToAction("Index");
        }
    }
}