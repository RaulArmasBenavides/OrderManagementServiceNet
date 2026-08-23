using OrderManagementService.Core.IRepository;

namespace OrderManagementService.Infrastructure.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
