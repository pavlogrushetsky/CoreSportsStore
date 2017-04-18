using CoreSportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreSportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List() => View(repository.Products);
    }
}