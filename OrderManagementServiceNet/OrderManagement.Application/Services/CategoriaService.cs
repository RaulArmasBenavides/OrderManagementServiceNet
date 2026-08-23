using AutoMapper;
using OrderManagementService.Application.Dtos;
using OrderManagementService.Application.Interfaces;
using OrderManagementService.Core.Entities;
using OrderManagementService.Infrastructure.Repository.UnitOfWork;

namespace OrderManagementService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);
            if (category == null) return false;
            _mapper.Map(dto, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(id);
            if (category == null) return false;
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
