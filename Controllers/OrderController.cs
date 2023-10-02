using BethanysPieShop.Models;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        public ActionResult Checkout()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)

        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if(_shoppingCart.ShoppingCartItems.Count==0)
            {
                ModelState.AddModelError("", "Your Cart is Empty Add Some Pie to Checkout");
            }
            else
            {
                if(ModelState.IsValid)
                {
                    _orderRepository.CreateOrder(order);
                    _shoppingCart.ClearCart();
                    return RedirectToAction("CheckoutComplete");
                }
            }

            return View(order);
        }
        public ActionResult CheckoutComplete()
        {
            ViewData["Message"] = "Enjoy your order and Let us know how does the pie taste like";
            return View();
        }
    }
}
