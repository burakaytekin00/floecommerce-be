using ECommerce.Core.Result;
using ECommerce.Entity;
using ECommerce.Entity.DTOs;
using ECommerce.Repository;

namespace ECommerce.Business
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<IEnumerable<UserTypeDto>> GetAll()
        {
            try
            {
                var userTypes = _unitOfWork.GetRepository<UserType>()
                    .GetAll()
                    .Where(x => !x.IsDeleted && x.IsActive)
                    .Select(ut => new UserTypeDto
                    {
                        Id = ut.Id,
                        UserTypeName = ut.UserTypeName,
                        UserTypeDescription = ut.UserTypeDescription,
                        CreatedDate = ut.CreatedDate,
                        UpdatedDate = ut.UpdatedDate,
                        IsActive = ut.IsActive,
                        IsDeleted = ut.IsDeleted
                    });

                return ApiResponse<IEnumerable<UserTypeDto>>.Success(userTypes);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<UserTypeDto>>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserTypeDto> GetById(int id)
        {
            try
            {
                var userType = _unitOfWork.GetRepository<UserType>()
                    .Find(x => x.Id == id && !x.IsDeleted && x.IsActive)
                    .Select(ut => new UserTypeDto
                    {
                        Id = ut.Id,
                        UserTypeName = ut.UserTypeName,
                        UserTypeDescription = ut.UserTypeDescription,
                        CreatedDate = ut.CreatedDate,
                        UpdatedDate = ut.UpdatedDate,
                        IsActive = ut.IsActive,
                        IsDeleted = ut.IsDeleted
                    })
                    .FirstOrDefault();

                if (userType == null)
                    return ApiResponse<UserTypeDto>.Fail("UserType not found");

                return ApiResponse<UserTypeDto>.Success(userType);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserTypeDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserTypeDto> Add(UserTypeDto userTypeDto)
        {
            try
            {
                var userType = new UserType
                {
                    UserTypeName = userTypeDto.UserTypeName,
                    UserTypeDescription = userTypeDto.UserTypeDescription
                };

                _unitOfWork.GetRepository<UserType>().Add(userType);
                _unitOfWork.SaveChanges();

                return GetById(userType.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserTypeDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<UserTypeDto> Update(UserTypeDto userTypeDto)
        {
            try
            {
                var userType = _unitOfWork.GetRepository<UserType>().GetById(userTypeDto.Id);
                if (userType == null || userType.IsDeleted)
                    return ApiResponse<UserTypeDto>.Fail("UserType not found");

                userType.UserTypeName = userTypeDto.UserTypeName;
                userType.UserTypeDescription = userTypeDto.UserTypeDescription;
                userType.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<UserType>().Update(userType);
                _unitOfWork.SaveChanges();

                return GetById(userType.Id);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserTypeDto>.Fail(ex.Message);
            }
        }

        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                var userType = _unitOfWork.GetRepository<UserType>().GetById(id);
                if (userType == null)
                    return ApiResponse<bool>.Fail("UserType not found");

                userType.IsDeleted = true;
                userType.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<UserType>().Update(userType);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "UserType successfully deleted");
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
                var userType = _unitOfWork.GetRepository<UserType>().GetById(id);
                if (userType == null)
                    return ApiResponse<bool>.Fail("UserType not found");

                userType.IsActive = isActive;
                userType.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.GetRepository<UserType>().Update(userType);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, $"UserType status successfully set to {isActive}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
    }
} 