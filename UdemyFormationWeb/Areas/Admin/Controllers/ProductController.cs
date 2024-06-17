using Microsoft.AspNetCore.Mvc;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormationWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfAction unitOfAction;
        public IProductRepository ProductRepository => unitOfAction.Product;

        public ProductController(IUnitOfAction unitOfAction)
        {
            this.unitOfAction = unitOfAction;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = ProductRepository.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Add(Product);
                unitOfAction.Save();
                TempData["success"] = "Product correctly created  WOWOWOOWOWO!!!";
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
            Product? Product = ProductRepository.Get(cat => cat.Id == id);
            if (Product is null)
            {
                return NotFound();
            }
            return View(Product);
        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Update(Product);
                unitOfAction.Save();
                TempData["success"] = "Product correctly updated  WOWOWOOWOWO!!!";
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
            Product? Product = ProductRepository.Get(cat => cat.Id == id);
            if (Product is null)
            {
                return NotFound();
            }

            return View(Product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? Product = ProductRepository.Get(cat => cat.Id == id);
            if (Product is null)
            {
                return NotFound();
            }
            ProductRepository.Remove(Product);
            unitOfAction.Save();
            TempData["success"] = "Product correctly deleted  WOWOWOOWOWO!!!";

            return RedirectToAction("Index");
        }
    }
}