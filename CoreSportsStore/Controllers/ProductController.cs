using CoreSportsStore.Models;
using CoreSportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreSportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public int PageSize = 4;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(int page = 1) 
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
    }
}