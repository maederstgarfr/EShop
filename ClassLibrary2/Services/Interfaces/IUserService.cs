using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Account;

namespace EShop.Application.Services.Interfaces
{
    interface IUserService : IAsyncDisposable
    {
        #region Register & login
        Task<RegisterOrLoginResult> RegisterOrLoginUser(RegisterUserDTO dto);
        Task<bool> CheckUserExistByMobile(string mobile);
        Task<EditUserInfoDTO> GetEditUserDetail(long userId);
        Task EditUserDetail(EditUserInfoDTO dto);
        Task<UserDetailDTO> GetUserDetail(long userId);
        Task<bool> SendActivationSms(string mobile);

        #endregion
    }
}
