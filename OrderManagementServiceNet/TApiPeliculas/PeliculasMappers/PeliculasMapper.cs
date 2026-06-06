using AutoMapper;
using OrderManagementService.Application.Dtos;
using OrderManagementService.Core.Entities;

namespace OrderManagementService.Mappers
{
    public class OrderManagementMapper : Profile
    {
        public OrderManagementMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : null));
            CreateMap<ProductDto, Product>().ForMember(d => d.Category, o => o.Ignore());
            CreateMap<CreateProductDto, Product>();

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User != null ? s.User.UserName : null));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product != null ? s.Product.Name : null));

            CreateMap<AppUser, UserDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));
        }
    }
}
