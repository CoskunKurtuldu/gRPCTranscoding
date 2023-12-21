using grpcService.Models;

namespace grpcService.Services.OrderServices
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> AddOrder(Order order)
        {
            if (order is not null)
            {
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
            }
            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> AllOrders()
        {
            return _dbContext.Orders.OrderByDescending(c => c.CreatedAt).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteOrderById(string Id)
        {
            var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == Id);
            if (orderObject is not null)
            {
                _dbContext.Orders.Remove(orderObject);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> EditOrder(Order order)
        {
            var orderObject = _dbContext.Orders.FirstOrDefault(c => c.Id == order.Id);
            if (orderObject is not null)
            {
                orderObject.Name = order.Name;
                orderObject.Enable = order.Enable;
                await _dbContext.SaveChangesAsync();
            }
            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Order GetOrderById(string Id) => _dbContext.Orders.FirstOrDefault(c => c.Id == Id);
    }
}
