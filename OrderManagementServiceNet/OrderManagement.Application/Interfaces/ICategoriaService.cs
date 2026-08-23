using OrderManagementService.Application.Dtos;

namespace OrderManagementService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
