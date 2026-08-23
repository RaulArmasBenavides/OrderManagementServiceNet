using System.ComponentModel.DataAnnotations;

namespace OrderManagementService.Application.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "registered";
    }
}
