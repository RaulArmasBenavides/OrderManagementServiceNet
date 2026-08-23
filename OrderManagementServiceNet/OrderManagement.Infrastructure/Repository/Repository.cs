using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using OrderManagementService.Core.IRepository;

namespace OrderManagementService.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        internal DbSet<T> _dbset;

        public Repository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(prop.Trim());

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(prop.Trim());

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbset.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync();
        }
    }
}
