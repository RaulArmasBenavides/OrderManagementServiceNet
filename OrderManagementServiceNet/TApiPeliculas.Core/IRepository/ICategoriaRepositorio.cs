using OrderManagementService.Core.Entities;

namespace OrderManagementService.Core.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<ICollection<Category>> GetCategoriesAsync();
    }
}
