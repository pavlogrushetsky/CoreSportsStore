using CoreSportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreSportsStore.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
