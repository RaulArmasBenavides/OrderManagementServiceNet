using Microsoft.EntityFrameworkCore;
using OrderManagementService.Core.Entities;
using OrderManagementService.Core.IRepository;
using OrderManagementService.Infrastructure.Repository.Data;

namespace OrderManagementService.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<ICollection<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _db.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string name)
        {
            return await _db.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(name) || p.Description.Contains(name))
                .ToListAsync();
        }
    }
}
