using System.Collections.Generic;
using System.Linq;

namespace CoreSportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var entry = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (entry != null)
                {
                    entry.Name = product.Name;
                    entry.Description = product.Description;
                    entry.Price = product.Price;
                    entry.Category = entry.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            var entry = context.Products.FirstOrDefault(p => p.ProductID == productID);
            if (entry != null)
            {
                context.Products.Remove(entry);
                context.SaveChanges();
            }
            return entry;
        }
    }
}