using BethanysPieShop.Models;

namespace BethanysPieShop.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
