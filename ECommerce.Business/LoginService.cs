using ECommerce.Core.Helpers;
using ECommerce.Core.Result;
using ECommerce.Entity;
using ECommerce.Entity.DTOs;
using ECommerce.Repository;
using ECommerce.Business.Abstract;

namespace ECommerce.Business
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher _passwordHasher;

        public LoginService(IUnitOfWork unitOfWork, PasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public ApiResponse<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = _unitOfWork.GetRepository<User>()
                    .Find(u => u.UserName == userLoginDto.UserName && !u.IsDeleted && u.IsActive)
                    .FirstOrDefault();

                if (user == null || !_passwordHasher.VerifyPassword(userLoginDto.Password, user.Password))
                {
                    return ApiResponse<UserLoginResponseDto>.Fail("Invalid username or password");
                }

                var response = new UserLoginResponseDto
                {
                    UserId = user.Id,
                    UserTypeId = user.UserTypeId
                };

                return ApiResponse<UserLoginResponseDto>.Success(response, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserLoginResponseDto>.Fail(ex.Message);
            }
        }
    }
} 