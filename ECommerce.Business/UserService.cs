using ECommerce.Core.Helpers;
using ECommerce.Core.Result;
using ECommerce.Entity;
using ECommerce.Entity.DTOs;
using ECommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Business
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, PasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public ApiResponse<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                var users = _unitOfWork.GetRepository<User>()
                    .GetAll()
                    .Where(x => !x.IsDeleted && x.IsActive)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        UserTypeId = u.UserTypeId,
                        UserName = u.UserName,
                        Name = u.Name,
                        Surname = u.Surname,
                        Address = u.Address,
                        MobilePhone = u.MobilePhone,
                        Email = u.Email,
                        CreatedDate = u.CreatedDate,
                        UpdatedDate = u.UpdatedDate,
                        IsActive = u.IsActive,
                        IsDeleted = u.IsDeleted
                    });

                return ApiResponse<IEnumerable<UserDto>>.Success(users);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<UserDto>>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserDto> GetById(int id)
        {
            try
            {
                var user = _unitOfWork.GetRepository<User>()
                    .Find(x => x.Id == id && !x.IsDeleted && x.IsActive)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        UserTypeId = u.UserTypeId,
                        UserName = u.UserName,
                        Name = u.Name,
                        Surname = u.Surname,
                        Address = u.Address,
                        MobilePhone = u.MobilePhone,
                        Email = u.Email,
                        CreatedDate = u.CreatedDate,
                        UpdatedDate = u.UpdatedDate,
                        IsActive = u.IsActive,
                        IsDeleted = u.IsDeleted
                    })
                    .FirstOrDefault();

                if (user == null)
                    return ApiResponse<UserDto>.Fail("User not found");

                return ApiResponse<UserDto>.Success(user);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserDto> Add(UserCreateDto userCreateDto)
        {
            try
            {
                var user = new User
                {
                    UserTypeId = userCreateDto.UserTypeId,
                    UserName = userCreateDto.UserName,
                    Password = _passwordHasher.HashPassword(userCreateDto.Password),
                    Name = userCreateDto.Name,
                    Surname = userCreateDto.Surname,
                    Address = userCreateDto.Address,
                    MobilePhone = userCreateDto.MobilePhone,
                    Email = userCreateDto.Email
                };

                _unitOfWork.GetRepository<User>().Add(user);
                _unitOfWork.SaveChanges();

                return GetById(user.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserDto> Update(UserDto userDto)
        {
            try
            {
                var user = _unitOfWork.GetRepository<User>().GetById(userDto.Id);
                if (user == null || user.IsDeleted)
                    return ApiResponse<UserDto>.Fail("User not found");

                user.UserTypeId = userDto.UserTypeId;
                user.UserName = userDto.UserName;
                user.Name = userDto.Name;
                user.Surname = userDto.Surname;
                user.Address = userDto.Address;
                user.MobilePhone = userDto.MobilePhone;
                user.Email = userDto.Email;
                user.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<User>().Update(user);
                _unitOfWork.SaveChanges();

                return GetById(user.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var user = _unitOfWork.GetRepository<User>().GetById(id);
                if (user == null)
                    return ApiResponse<bool>.Fail("User not found");

                user.IsDeleted = true;
                user.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<User>().Update(user);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "User successfully deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> SetStatus(int id, bool isActive)
        {
            try
            {
                var user = _unitOfWork.GetRepository<User>().GetById(id);
                if (user == null)
                    return ApiResponse<bool>.Fail("User not found");

                user.IsActive = isActive;
                user.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<User>().Update(user);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, $"User status successfully set to {isActive}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
    }
} 