using BethanysPieShop.Migrations;
using BethanysPieShop.Repositories;

namespace BethanysPieShop.ModelViews
{
    public class ShoppingCartViewModel
    {

        public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal) {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }

        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; }
    }
}
