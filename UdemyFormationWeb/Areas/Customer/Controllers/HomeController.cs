using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormationWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfAction unitOfAction;

        public HomeController(ILogger<HomeController> logger, IUnitOfAction unitOfAction)
        {
            this.unitOfAction = unitOfAction;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = unitOfAction.Product.GetAll(null);
            return View(products);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = unitOfAction.Product.Get((Product p) => p.Id == id, "Category");
            if (product == null)
            {
                return NotFound();
            }
            ProductDetailsVM vm = new() { Count = 1, Product = product };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ProductDetailsVM productDetailsVM)
        {
            if (productDetailsVM.Product == null)
            {
                return NotFound();
            }
            var claimedIdendity = (ClaimsIdentity)User.Identity;
            string userId = claimedIdendity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCart cart = new() { ProductId = productDetailsVM.Product.Id, UserId = userId, Count = productDetailsVM.Count };

            unitOfAction.Cart.Add(cart);
            HttpContext.Session.SetInt32("cart", unitOfAction.Cart.GetAll().Count());

            unitOfAction.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}