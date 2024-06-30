using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UdemyFormation.DataAccess.Repository.IRepository;

namespace UdemyFormationWeb.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        private readonly IUnitOfAction _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfAction unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {

                if (HttpContext.Session.GetInt32("cart") == null)
                {
                    HttpContext.Session.SetInt32("cart",
                    _unitOfWork.Cart.GetAll(u => u.UserId == claim.Value).Count());
                }

                return View(HttpContext.Session.GetInt32("cart"));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
