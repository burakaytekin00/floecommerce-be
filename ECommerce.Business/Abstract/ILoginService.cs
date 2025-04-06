using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface ILoginService
    {
        ApiResponse<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    }
} 