using BethanysPieShop.Data;
using BethanysPieShop.Models;

namespace BethanysPieShop.Repositories
{
    public class OrderRepository :IOrderRepository
    {

            private readonly ApplicationDbContext _context;
            private readonly IShoppingCart _shoppingCart;

            public OrderRepository(ApplicationDbContext context, IShoppingCart shoppingCart)
            {
                _context = context;
                _shoppingCart = shoppingCart;
            }

            public void CreateOrder(Order order)
            {
                order.OrderPlaced = DateTime.Now;

                List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

                order.OrderDetails = new List<OrderDetail>();

                //adding the order with its details

                foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Amount = shoppingCartItem.Amount,
                        PieId = shoppingCartItem.Pie.PieId,
                        Price = shoppingCartItem.Pie.Price
                    };

                    order.OrderDetails.Add(orderDetail);
                }

                _context.Orders.Add(order);

                _context.SaveChanges();
            }
        
    }
}