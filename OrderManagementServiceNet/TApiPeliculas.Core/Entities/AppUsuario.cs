using Microsoft.AspNetCore.Identity;

namespace OrderManagementService.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
