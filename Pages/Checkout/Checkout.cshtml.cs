using BethanysPieShop.Models;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages.Checkout
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        private readonly IShoppingCart _shoppingCartRepo;
        private readonly IOrderRepository _orderRepository;
        public CheckoutModel(IShoppingCart shoppingCartRepo, IOrderRepository orderRepository)
        {
            _shoppingCartRepo=shoppingCartRepo;
            _orderRepository= orderRepository; ;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var items = _shoppingCartRepo.GetShoppingCartItems();
            _shoppingCartRepo.ShoppingCartItems = items;

            if (_shoppingCartRepo.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your Cart is Empty Add Some Pie to Checkout");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _orderRepository.CreateOrder(Order);
                    _shoppingCartRepo.ClearCart();
                    return RedirectToPage("CheckoutCompletePage");
                }
            }
            return Page();

        }
    }
}
