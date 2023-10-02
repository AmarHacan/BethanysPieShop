using BethanysPieShop.ModelViews;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCart _shoppingCartRepository;

        public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCartRepository) {
            _pieRepository = pieRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public ViewResult Index()
        {
            var items= _shoppingCartRepository.GetShoppingCartItems();
            _shoppingCartRepository.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCartRepository,_shoppingCartRepository.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie= _pieRepository.AllPies.Where(i=>pieId==i.PieId).FirstOrDefault();
            if (selectedPie != null)
            {
                _shoppingCartRepository.AddToCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.Where(i => pieId == i.PieId).FirstOrDefault();
            if (selectedPie != null)
            {
                _shoppingCartRepository.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

    }
}
