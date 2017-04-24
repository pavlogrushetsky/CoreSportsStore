using System.Collections.Generic;

namespace CoreSportsStore.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; set; }
        void SaveOrder(Order order);
    }
}
