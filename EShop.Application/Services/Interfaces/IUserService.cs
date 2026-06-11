using System;
using System.Threading.Tasks;
using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region Register & login
        Task RegisterOrLoginUser(RegisterUserDTO dto);
        Task<bool> CheckUserExistByMobile(string mobile);
        Task<EditUserInfoDTO> GetEditUserDetail(long userId);
        Task EditUserDetail(EditUserInfoDTO dto);
        Task<UserDetailDTO> GetUserDetail(long userId);
        Task<bool> SendActivationSMS(string mobile);
        Task<bool> CheckMobileAuthorization(MobileActivationDTO dto);
        Task<User> GetUserByMobile(string mobile);

        #endregion
    }
}