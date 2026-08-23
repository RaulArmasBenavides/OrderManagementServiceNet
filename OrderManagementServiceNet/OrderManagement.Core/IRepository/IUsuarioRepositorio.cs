using OrderManagementService.Core.Entities;

namespace OrderManagementService.Core.IRepository
{
    public interface IUserRepository
    {
        Task<ICollection<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserAsync(string id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<bool> IsUniqueUserAsync(string username);
    }
}
