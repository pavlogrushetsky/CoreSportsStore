using System.Linq;
using CoreSportsStore.Models;
using CoreSportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreSportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                cart.AddItem(product, 1);

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId,
            string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
                cart.RemoveLine(product);

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}