using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            var products = unitOfAction.Product.GetAll();
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
            return View(product);
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