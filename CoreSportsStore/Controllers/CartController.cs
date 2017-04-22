using System.Linq;
using CoreSportsStore.Infrastructure;
using CoreSportsStore.Models;
using CoreSportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreSportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;

        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
                return RedirectToAction("Index", new {returnUrl});

            var cart = GetCart();
            cart.AddItem(product, 1);
            SaveCart(cart);

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId,
            string returnUrl)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
                return RedirectToAction("Index", new {returnUrl});

            var cart = GetCart();
            cart.RemoveLine(product);
            SaveCart(cart);
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}