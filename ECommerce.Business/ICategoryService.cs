using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business
{
    public interface ICategoryService
    {
        ApiResponse<IEnumerable<CategoryDto>> GetAllCategories();
        ApiResponse<CategoryDto> GetCategoryById(int id);
        ApiResponse<CategoryDto> AddCategory(CategoryDto categoryDto);
        ApiResponse<CategoryDto> UpdateCategory(CategoryDto categoryDto);
        ApiResponse<bool> DeleteCategory(int id);
        ApiResponse<bool> SetCategoryStatus(int id, bool isActive);
    }
}
