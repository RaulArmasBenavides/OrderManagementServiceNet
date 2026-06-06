using OrderManagementService.Application.Dtos;

namespace OrderManagementService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(string userId);
        Task<OrderDto> CreateOrderAsync(string userId, CreateOrderDto dto);
        Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
