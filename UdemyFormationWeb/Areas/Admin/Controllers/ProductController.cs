using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;
using System.Web;
using Azure.Core.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using UdemyFormation.Utility;

namespace UdemyFormationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Consts.Admin_Role)]
    public class ProductController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        private IUnitOfAction unitOfAction;
        public IProductRepository ProductRepository => unitOfAction.Product;

        public ProductController(IUnitOfAction unitOfAction, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfAction = unitOfAction;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = ProductRepository.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            Product product = (id == null || id == 0) ? new Product() : unitOfAction.Product.Get(p => p.Id == id);
            SetCategoriesDropdown();
            return View(product);
        }

        [HttpPost]
        public IActionResult Upsert(Product Product, IFormFile? file)
        {
            bool isCreate = Product.Id == 0;
            if (ModelState.IsValid)
            {
                if (file is not null)
                {
                    string rootPath = webHostEnvironment.WebRootPath;
                    string relativeImageFolderPath = Path.Combine("Images", "Product");

                    if (!isCreate && !string.IsNullOrEmpty(Product.ImagePath))
                    {
                        DeleteImage(Product.ImagePath);
                    }

                    string newImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string newImageRelativePath = Path.Combine(relativeImageFolderPath, newImageName);
                    string newImageAbsolutePath = Path.Combine(rootPath, newImageRelativePath);

                    using (var fileStream = new FileStream(newImageAbsolutePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    Product.ImagePath = @"\" + newImageRelativePath;
                }

                if (isCreate)
                {
                    ProductRepository.Add(Product);
                }
                else
                {
                    ProductRepository.Update(Product);
                }

                unitOfAction.Save();
                TempData["success"] = $"Product correctly {(isCreate ? "Create" : "Update")}  WOWOWOOWOWO!!!";
                return RedirectToAction("Index");
            }
            SetCategoriesDropdown();
            return View(Product);
        }

        private void DeleteImage(string relativeImagePath)
        {
            string rootPath = webHostEnvironment.WebRootPath;
            string imageAbsolutePath = Path.Combine(rootPath, relativeImagePath.TrimStart('\\'));
            if (System.IO.File.Exists(imageAbsolutePath))
            {
                System.IO.File.Delete(imageAbsolutePath);
            }
        }

        [HttpDelete]
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
            if (!string.IsNullOrEmpty(Product.ImagePath))
            {
                DeleteImage(Product.ImagePath);
            }

            ProductRepository.Remove(Product);
            unitOfAction.Save();
            //TempData["success"] = "Product correctly deleted  WOWOWOOWOWO!!!";
            return Json(new { success = true, message = "Delete successful" });
            //return RedirectToAction("Index");
        }

        private void SetCategoriesDropdown()
        {
            IEnumerable<SelectListItem> selectListItems =
                unitOfAction.Category.GetAll().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            ViewBag.CategoriesDropdown = selectListItems;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = ProductRepository.GetAll("Category");

            return Json(new { data = products });
        }
    }
}