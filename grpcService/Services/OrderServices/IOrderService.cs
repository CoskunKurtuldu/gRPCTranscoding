using grpcService.Models;

namespace grpcService.Services.OrderServices
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
        Task<Order> EditOrder(Order order);
        IEnumerable<Order> AllOrders();
        Order GetOrderById(string Id);
        bool DeleteOrderById(string Id);
    }
}
