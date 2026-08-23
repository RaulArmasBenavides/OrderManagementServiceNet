using OrderManagementService.Application.Dtos;

namespace OrderManagementService.Application.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserAsync(string id);
        Task<UserDto?> RegisterAsync(RegisterDto dto);
        Task<LoginResponseDto> LoginAsync(LoginDto dto, string secretKey);
    }
}
