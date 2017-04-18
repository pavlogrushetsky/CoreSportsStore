using System.Collections.Generic;

namespace CoreSportsStore.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}