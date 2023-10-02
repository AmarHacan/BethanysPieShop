
using BethanysPieShop.Data;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public string ShoppingCartId { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
                ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
                ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>() ?? throw new Exception("Error Initializing");
                string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
                session?.SetString("CartId", cartId);
            Console.WriteLine(cartId);
                return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem= _context.ShoppingCartItems.SingleOrDefault(sci=>sci.Pie.PieId==pie.PieId && sci.ShoppingCartId==ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1,
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }


        public void ClearCart()
        {
            var shoppingcart = _context.ShoppingCartItems.Where(cart=>cart.ShoppingCartId == ShoppingCartId).FirstOrDefault();
            _context.ShoppingCartItems.RemoveRange(shoppingcart);
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _context.ShoppingCartItems.Where(items => items.ShoppingCartId == ShoppingCartId).Include(pieproduct => pieproduct.Pie).ToList();        }

        public decimal GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(i=>i.ShoppingCartId==ShoppingCartId).Select(c=>c.Pie.Price*c.Amount).Sum();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(i => i.Pie == pie && i.ShoppingCartId==ShoppingCartId);

            var localAmount = 0;
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }

    }
}
