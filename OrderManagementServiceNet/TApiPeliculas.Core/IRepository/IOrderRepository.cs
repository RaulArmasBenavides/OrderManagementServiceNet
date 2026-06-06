using OrderManagementService.Core.Entities;

namespace OrderManagementService.Core.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<ICollection<Order>> GetOrdersAsync();
        Task<Order?> GetOrderWithItemsAsync(int orderId);
        Task<ICollection<Order>> GetOrdersByUserAsync(string userId);
    }
}
