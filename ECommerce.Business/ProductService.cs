using ECommerce.Entity;
using ECommerce.Entity.DTOs;
using ECommerce.Repository;
using ECommerce.Core.Result;
using ECommerce.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Business
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<ProductDto>> GetAll()
        {
            try
            {
                var products = _unitOfWork.GetRepository<Product>()
                    .GetAll()
                    .Include(x => x.Category)
                    .Where(x => !x.IsDeleted && x.IsActive)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.Name,
                        Name = p.Name,
                        Description = p.Description,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        CreatedDate = p.CreatedDate,
                        UpdatedDate = p.UpdatedDate,
                        IsActive = p.IsActive,
                        IsDeleted = p.IsDeleted
                    });

                return ApiResponse<IEnumerable<ProductDto>>.Success(products);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProductDto>>.Fail(ex.Message);
            }
        }

        public ApiResponse<IEnumerable<ProductDto>> GetAllByFilter(ProductFilterDto filter)
        {
            try
            {
                var query = _unitOfWork.GetRepository<Product>()
                    .GetAll()
                    .Include(x => x.Category)
                    .Where(x => !x.IsDeleted && x.IsActive);

                if (filter.CategoryId.HasValue)
                {
                    query = query.Where(x => x.CategoryId == filter.CategoryId.Value);
                }

                if (!string.IsNullOrEmpty(filter.SearchText))
                {
                    query = query.Where(x => x.Name.Contains(filter.SearchText) || x.Description.Contains(filter.SearchText));
                }

                var products = query.Select(p => new ProductDto
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Name = p.Name,
                    Description = p.Description,
                    PhotoUrl = p.PhotoUrl,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    IsActive = p.IsActive,
                    IsDeleted = p.IsDeleted
                });

                return ApiResponse<IEnumerable<ProductDto>>.Success(products);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProductDto>>.Fail(ex.Message);
            }
        }

        public ApiResponse<ProductDto> GetById(int id)
        {
            try
            {
                var product = _unitOfWork.GetRepository<Product>()
                    .Find(x => x.Id == id && !x.IsDeleted && x.IsActive)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        CategoryId = p.CategoryId,
                        Name = p.Name,
                        Description = p.Description,
                        PhotoUrl = p.PhotoUrl,
                        Price = p.Price,
                        CreatedDate = p.CreatedDate,
                        UpdatedDate = p.UpdatedDate,
                        IsActive = p.IsActive,
                        IsDeleted = p.IsDeleted
                    })
                    .FirstOrDefault();

                if (product == null)
                    return ApiResponse<ProductDto>.Fail("Product not found");

                return ApiResponse<ProductDto>.Success(product);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<ProductDto> Add(ProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    CategoryId = productDto.CategoryId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    PhotoUrl = productDto.PhotoUrl,
                    Price = productDto.Price
                };

                _unitOfWork.GetRepository<Product>().Add(product);
                _unitOfWork.SaveChanges();

                return GetById(product.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<ProductDto> Update(ProductDto productDto)
        {
            try
            {
                var product = _unitOfWork.GetRepository<Product>().GetById(productDto.Id);
                if (product == null || product.IsDeleted)
                    return ApiResponse<ProductDto>.Fail("Product not found");

                product.CategoryId = productDto.CategoryId;
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.PhotoUrl = productDto.PhotoUrl;
                product.Price = productDto.Price;
                product.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Product>().Update(product);
                _unitOfWork.SaveChanges();

                return GetById(product.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var product = _unitOfWork.GetRepository<Product>().GetById(id);
                if (product == null)
                    return ApiResponse<bool>.Fail("Product not found");

                product.IsDeleted = true;
                product.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Product>().Update(product);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "Product successfully deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> SetProductStatus(int id, bool isActive)
        {
            try
            {
                var product = _unitOfWork.GetRepository<Product>().GetById(id);
                if (product == null)
                    return ApiResponse<bool>.Fail("Product not found");

                product.IsActive = isActive;
                product.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<Product>().Update(product);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, $"Product status successfully set to {isActive}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
    }
}
