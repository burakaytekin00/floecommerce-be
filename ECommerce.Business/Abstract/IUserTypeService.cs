using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface IUserTypeService
    {
        ApiResponse<IEnumerable<UserTypeDto>> GetAll();
        ApiResponse<UserTypeDto> GetById(int id);
        ApiResponse<UserTypeDto> Add(UserTypeDto userTypeDto);
        ApiResponse<UserTypeDto> Update(UserTypeDto userTypeDto);
        ApiResponse<bool> Delete(int id);
        ApiResponse<bool> SetStatus(int id, bool isActive);
    }
} 