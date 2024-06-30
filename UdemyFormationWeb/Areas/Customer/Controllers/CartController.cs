using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormationWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUnitOfAction unitOfAction;

        public CartController(ILogger<CartController> logger, IUnitOfAction unitOfAction)
        {
            this.unitOfAction = unitOfAction;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var claimedIdendity = (ClaimsIdentity)User.Identity;
            string userId = claimedIdendity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = unitOfAction.Cart.GetAll(c => c.UserId == userId, "Product");
            ShoppingCartVM vm = new() { ShoppingCartList = carts };
            foreach (var cart in carts)
            {
                cart.Price = cart.Count < 10 ? cart.Product.Price : cart.Product.Price10;
            }
            ViewBag.OrderTotal = carts.Sum(c => c.Price);
            return View(vm);
        }

        public IActionResult Plus(int? cartid)
        {
            if (cartid is null || cartid == 0)
            {
                return NotFound();
            }

            ShoppingCart shoppingCart = unitOfAction.Cart.Get(c => c.Id == cartid);
            shoppingCart.Count++;
            unitOfAction.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int? cartid)
        {
            if (cartid is null || cartid == 0)
            {
                return NotFound();
            }

            ShoppingCart shoppingCart = unitOfAction.Cart.Get(c => c.Id == cartid);
            if (shoppingCart.Count > 1)
            {
                shoppingCart.Count--;
            }
            else
            {
                unitOfAction.Cart.Remove(shoppingCart);
            }
            unitOfAction.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int? cartid)
        {
            if (cartid is null || cartid == 0)
            {
                return NotFound();
            }

            ShoppingCart shoppingCart = unitOfAction.Cart.Get(c => c.Id == cartid);

            unitOfAction.Cart.Remove(shoppingCart);
            unitOfAction.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimedIdendity = (ClaimsIdentity)User.Identity;
            string userId = claimedIdendity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = unitOfAction.Cart.GetAll(c => c.UserId == userId, "Product");
            ShoppingCartVM vm = new() { ShoppingCartList = carts };
            foreach (var cart in carts)
            {
                cart.Price = cart.Count < 10 ? cart.Product.Price : cart.Product.Price10;
            }
            ViewBag.OrderTotal = carts.Sum(c => c.Price);
            return View(vm);
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