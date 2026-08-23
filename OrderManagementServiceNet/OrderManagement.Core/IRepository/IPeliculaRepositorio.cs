using OrderManagementService.Core.Entities;

namespace OrderManagementService.Core.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetProductsAsync();
        Task<ICollection<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsAsync(string name);
    }
}
