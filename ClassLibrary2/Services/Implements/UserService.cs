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
    public class IUserService : Interfaces.IUserService
    {
        #region CTOR
        private readonly IGenericRipository<User> _userRepository;
        private readonly ISmsService _SmsService;

        public IUserService(IGenericRipository<User> userRepository)
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
                await _SmsService.SendVerificationSms(dto.MobileNumber, user.MobileActivationNumber);
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
                await _SmsService.SendVerificationSms(dto.MobileNumber, newUser.MobileActivationNumber);
                return RegisterOrLoginResult.MobileInUse;
            }
            
        }

        public async Task<bool> CheckUserExistByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.MobileNumber == mobile);
        }



        public async Task EditUserDetail(EditUserInfoDTO dto)
        {
            var user = await  _userRepository.GetEntityById(dto.UserId);
            user.Address = dto.Address;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PostCode = dto.PostCode;


            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();
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

        public async Task<UserDetailDTO> GetUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById( userId);
            return new UserDetailDTO
            {
                Id = userId,
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PostCode = user.PostCode,
                MobileNumber = user.MobileNumber,
                CreateDate = user.CreateDate,
                LastUpdateDate = user.LastUpdateDate,
                IsDeleted = user.IsDeleted,
                MobileActivationNumber = user.MobileActivationNumber,
            };

        }

        public async Task<bool> SendActivationSms(string mobile)
        {   
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
            if (user == null) return false;

            user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
            await _SmsService.SendVerificationSms(mobile, user.MobileActivationNumber);
            return true;
            
        }

        public async Task<bool> CheckMobileAuthorization(MobileActivationDTO dto)
        {
            var user = await GetUserByMobile(dto.mobile);
            return dto.ActivationCode == user.MobileActivationNumber;

        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
        }

        #endregion


    }
}
