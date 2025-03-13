using ECommerce.Core.Result;
using ECommerce.Entity.DTOs;

namespace ECommerce.Business
{
    public interface ILoginService
    {
        ApiResponse<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    }
} 