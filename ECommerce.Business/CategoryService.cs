using ECommerce.Entity;
using ECommerce.Entity.DTOs;
using ECommerce.Repository;
using ECommerce.Core.Result;

namespace ECommerce.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = _unitOfWork.GetRepository<Category>()
                    .GetAll()
                    .Where(x => !x.IsDeleted && x.IsActive)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedDate = c.CreatedDate,
                        UpdatedDate = c.UpdatedDate,
                        IsActive = c.IsActive,
                        IsDeleted = c.IsDeleted
                    });

                return ApiResponse<IEnumerable<CategoryDto>>.Success(categories);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<CategoryDto>>.Fail(ex.Message);
            }
        }

        public ApiResponse<CategoryDto> GetCategoryById(int id)
        {
            try
            {
                var category = _unitOfWork.GetRepository<Category>()
                    .Find(x => x.Id == id && !x.IsDeleted && x.IsActive)
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        CreatedDate = c.CreatedDate,
                        UpdatedDate = c.UpdatedDate,
                        IsActive = c.IsActive,
                        IsDeleted = c.IsDeleted
                    })
                    .FirstOrDefault();

                if (category == null)
                    return ApiResponse<CategoryDto>.Fail("Category not found");

                return ApiResponse<CategoryDto>.Success(category);
            }
            catch (Exception ex)
            {
                return ApiResponse<CategoryDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<CategoryDto> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                var existingCategory = _unitOfWork.GetRepository<Category>()
                .Find(x => x.Name == categoryDto.Name);

                if (existingCategory.Count() > 0)
                {
                    var response = new ApiResponse<CategoryDto>
                    {
                        IsSuccess = false,
                        Message = "Bu kategori zaten mevcut."
                    };
                    return response;
                }

                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description
                };

                _unitOfWork.GetRepository<Category>().Add(category);
                _unitOfWork.SaveChanges();

                var result = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedDate = category.CreatedDate,
                    UpdatedDate = category.UpdatedDate,
                    IsActive = category.IsActive,
                    IsDeleted = category.IsDeleted
                };

                return ApiResponse<CategoryDto>.Success(result, "Kategori başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<CategoryDto>.Fail($"Bir hata oluştu: {ex.Message}");
            }
        }

        public ApiResponse<CategoryDto> UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = _unitOfWork.GetRepository<Category>().GetById(categoryDto.Id);
                if (category == null || category.IsDeleted)
                    return ApiResponse<CategoryDto>.Fail("Category not found");

                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Category>().Update(category);
                _unitOfWork.SaveChanges();

                var result = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedDate = category.CreatedDate,
                    UpdatedDate = category.UpdatedDate,
                    IsActive = category.IsActive,
                    IsDeleted = category.IsDeleted
                };

                return ApiResponse<CategoryDto>.Success(result, "Category successfully updated");
            }
            catch (Exception ex)
            {
                return ApiResponse<CategoryDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> DeleteCategory(int id)
        {
            try
            {
                var category = _unitOfWork.GetRepository<Category>().GetById(id);
                if (category == null)
                    return ApiResponse<bool>.Fail("Category not found");

                category.IsDeleted = true;
                category.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Category>().Update(category);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "Category successfully deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> SetCategoryStatus(int id, bool isActive)
        {
            try
            {
                var category = _unitOfWork.GetRepository<Category>().GetById(id);
                if (category == null)
                    return ApiResponse<bool>.Fail("Category not found");

                category.IsActive = isActive;
                category.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Category>().Update(category);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, $"Category status successfully set to {isActive}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
    }
}
