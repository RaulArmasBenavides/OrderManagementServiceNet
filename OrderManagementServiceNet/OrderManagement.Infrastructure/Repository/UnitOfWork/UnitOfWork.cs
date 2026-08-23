using OrderManagementService.Core.IRepository;
using OrderManagementService.Infrastructure.Repository;
using OrderManagementService.Infrastructure.Repository.Data;

namespace OrderManagementService.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            Products = new ProductRepository(_db);
            Orders = new OrderRepository(_db);
            Users = new UserRepository(_db);
        }

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IUserRepository Users { get; private set; }

        public void Dispose() => _db.Dispose();

        public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
