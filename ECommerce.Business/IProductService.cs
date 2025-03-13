using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business
{
    public interface IProductService
    {
        ApiResponse<IEnumerable<ProductDto>> GetAll();
        ApiResponse<ProductDto> GetById(int id);
        ApiResponse<ProductDto> Add(ProductDto productDto);
        ApiResponse<ProductDto> Update(ProductDto productDto);
        ApiResponse<bool> Delete(int id);
        ApiResponse<bool> SetProductStatus(int id, bool isActive);
    }
}
