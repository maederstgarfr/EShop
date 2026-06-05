using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;
using EShop.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region CTOR
        private readonly IGenericRipository<User> _userRepository;
        private readonly ISMSService _SmsService;

        public UserService(IGenericRipository<User> userRepository)
        {
            _userRepository = userRepository;

        }
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }

        #endregion



        #region Register Methods
        public async Task<RegisterOrLoginResult> RegisterOrLoginUser(RegisterUserDTO dto)
        {
            var checkUser = await CheckUserExistByMobile(dto.MobileNumber);

            if (checkUser)
            {
                var user = await _userRepository.GetQuery().FirstAsync(u => u.MobileNumber == dto.MobileNumber);
                user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
                _userRepository.EditEntity(user);
                await _userRepository.SaveAsync();
                await _SmsService.SendVerificationSMS(dto.MobileNumber, user.MobileActivationNumber);
                return RegisterOrLoginResult.Success;
            }
            else
            {
                var newUser = new User
                {
                    MobileNumber = dto.MobileNumber,
                    MobileActivationNumber = new Random().Next(10000, 99999).ToString(),
                };
                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveAsync();
                await _SmsService.SendVerificationSMS(dto.MobileNumber, newUser.MobileActivationNumber);
                return RegisterOrLoginResult.MobileInUse;
            }
            
        }

        public async Task<bool> CheckUserExistByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.MobileNumber == mobile);
        }



        public Task EditUserDetail(EditUserInfoDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<EditUserInfoDTO> GetEditUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            return new EditUserInfoDTO
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PostCode = user.PostCode
            };
        }

        public Task<UserDetailDTO> GetUserDetail(long userId)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> SendActivationSMS(string mobile)
        {
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
            if (user == null) return false;

            user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
            await _SmsService.SendVerificationSMS(mobile, user.MobileActivationNumber);
            return true;
        }

        #endregion


    }
}
