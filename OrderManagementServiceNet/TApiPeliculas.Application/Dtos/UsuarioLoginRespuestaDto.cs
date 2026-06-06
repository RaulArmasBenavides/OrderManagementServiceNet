namespace OrderManagementService.Application.Dtos
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
