using System.Linq;
using CoreSportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CoreSportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) => View(
            repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deleted = repository.DeleteProduct(productId);
            if (deleted != null)
            {
                TempData["message"] = $"{deleted.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
