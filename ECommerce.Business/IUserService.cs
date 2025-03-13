using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business
{
    public interface IUserService
    {
        ApiResponse<IEnumerable<UserDto>> GetAll();
        ApiResponse<UserDto> GetById(int id);
        ApiResponse<UserDto> Add(UserCreateDto userCreateDto);
        ApiResponse<UserDto> Update(UserDto userDto);
        ApiResponse<bool> Delete(int id);
        ApiResponse<bool> SetStatus(int id, bool isActive);
    }
} 