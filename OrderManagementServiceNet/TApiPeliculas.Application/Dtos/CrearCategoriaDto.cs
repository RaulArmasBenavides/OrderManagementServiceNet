using System.ComponentModel.DataAnnotations;

namespace OrderManagementService.Application.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(60, ErrorMessage = "Maximum 60 characters allowed")]
        public string Name { get; set; } = string.Empty;
    }
}
