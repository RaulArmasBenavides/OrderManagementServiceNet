using AutoMapper;
using OrderManagementService.Application.Dtos;
using OrderManagementService.Application.Interfaces;
using OrderManagementService.Core.Entities;
using OrderManagementService.Infrastructure.Repository.UnitOfWork;

namespace OrderManagementService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(string userId, CreateOrderDto dto)
        {
            var order = new Order
            {
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..4].ToUpper()}",
                UserId = userId,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.Products.GetAsync(item.ProductId);
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product?.Price ?? 0
                });
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var created = await _unitOfWork.Orders.GetOrderWithItemsAsync(order.Id);
            return _mapper.Map<OrderDto>(created!);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto)
        {
            var order = await _unitOfWork.Orders.GetAsync(id);
            if (order == null) return false;
            order.Status = dto.Status;
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetAsync(id);
            if (order == null) return false;
            _unitOfWork.Orders.Remove(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
