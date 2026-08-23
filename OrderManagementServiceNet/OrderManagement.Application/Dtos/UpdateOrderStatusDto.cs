using System.ComponentModel.DataAnnotations;
using OrderManagementService.Core.Entities;

namespace OrderManagementService.Application.Dtos
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public OrderStatus Status { get; set; }
    }
}
