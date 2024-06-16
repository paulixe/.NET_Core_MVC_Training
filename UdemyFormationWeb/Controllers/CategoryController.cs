using Microsoft.AspNetCore.Mvc;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormationWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = categoryRepository.GetAll().ToList();
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
                categoryRepository.Add(category);
                categoryRepository.Save();
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
            Category? category = categoryRepository.Get(cat => cat.Id == id);
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
                categoryRepository.Update(category);
                categoryRepository.Save();
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
            Category? category = categoryRepository.Get(cat => cat.Id == id);
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
            Category? category = categoryRepository.Get(cat => cat.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            categoryRepository.Remove(category);
            categoryRepository.Save();
            TempData["success"] = "Category correctly deleted  WOWOWOOWOWO!!!";

            return RedirectToAction("Index");
        }
    }
}