using Abeslamidze_Web.Models;

using Abeslamidze_Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Abeslamidze_Web.Components
{
	public class CartViewComponent : ViewComponent
	{
		private Cart _cart;
		public CartViewComponent(Cart cart)
		{
			_cart = cart;
		}
		public IViewComponentResult Invoke()
		{
			var cart = HttpContext.Session.Get<Cart>("cart");
			return View(cart);
		}
	}
}
