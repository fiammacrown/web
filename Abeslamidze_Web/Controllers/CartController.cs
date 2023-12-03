using Abeslamidze_Web.DAL.Data;
using Abeslamidze_Web.Extensions;
using Abeslamidze_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abeslamidze_Web.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private Cart _cart;

        private string cartKey = "cart";
        public CartController(ApplicationDbContext context, Cart cart)
		{
            _context = context;
			_cart = cart;
		}

        public IActionResult Index()
        {
            return View(_cart.Items.Values);
        }

		[Authorize]
		public IActionResult Add(int id, string returnUrl)
		{
			var item = _context.Dishes.Find(id);
			if (item != null)
			{
				_cart.AddToCart(item);
			}
			return Redirect(returnUrl);
		}
		public IActionResult Delete(int id)
		{
			_cart.RemoveFromCart(id);
			return RedirectToAction("Index");
		}
	}
}
