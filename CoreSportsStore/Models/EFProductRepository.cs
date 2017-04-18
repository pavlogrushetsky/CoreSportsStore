using System.Collections.Generic;

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
    }
}