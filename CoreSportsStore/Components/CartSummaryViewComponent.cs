using CoreSportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreSportsStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;

        public CartSummaryViewComponent(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
