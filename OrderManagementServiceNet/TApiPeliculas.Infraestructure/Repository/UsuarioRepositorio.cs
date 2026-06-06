using Microsoft.EntityFrameworkCore;
using OrderManagementService.Core.Entities;
using OrderManagementService.Core.IRepository;
using OrderManagementService.Infrastructure.Repository.Data;

namespace OrderManagementService.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<AppUser>> GetUsersAsync()
        {
            return await _db.AppUsers.OrderBy(u => u.FullName).ToListAsync();
        }

        public async Task<AppUser?> GetUserAsync(string id)
        {
            return await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await _db.AppUsers.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> IsUniqueUserAsync(string username)
        {
            return !await _db.AppUsers.AnyAsync(u => u.UserName == username);
        }
    }
}
