using System;
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
        private readonly ISmsService _SmsService;

        public UserService(IGenericRipository<User> userRepository, ISmsService SmsService)
        {
            _userRepository = userRepository;
            _SmsService = SmsService;

        }
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }

        #endregion

        #region Register Methods
        public async Task RegisterOrLoginUser(RegisterUserDTO dto)
        {
            var checkUser = await CheckUserExistByMobile(dto.MobileNumber);

            if (checkUser)
            {
                var user = await _userRepository.GetQuery().FirstAsync(u => u.MobileNumber == dto.MobileNumber);
                user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
                _userRepository.EditEntity(user);
                await _userRepository.SaveAsync();
                await _SmsService.SendVerificationSMS(dto.MobileNumber, user.MobileActivationNumber);
                return;
            }
            var newUser = new User
            {
                MobileNumber = dto.MobileNumber,
                MobileActivationNumber = new Random().Next(10000, 99999).ToString(),
            };
            await _userRepository.AddEntity(newUser);
            await _userRepository.SaveAsync();
            await _SmsService.SendVerificationSMS(dto.MobileNumber, newUser.MobileActivationNumber);


        }

        public async Task<bool> CheckUserExistByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.MobileNumber == mobile);
        }

        public async Task EditUserDetail(EditUserInfoDTO dto)
        {
            // با ایدی تعیین میکنیم کدوم یوزر و میخواهیم تغییر بدیم
            var user = await _userRepository.GetEntityById(dto.UserId);

            // و روی یوزر تغییر میدیم
            //مقداری که از قبل بوده برابر میشه با مقداری که کاربر تغییر داده(dto مقدار تغییر کرده هست)
            user.Address = dto.Address;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PostCode = dto.PostCode;

            //اپدیت async نیست باید غیر همزمان باشه که مثلا یک مورد و دو ادمین همزمان ویرایش نکنند
            //مثلا محصول

            _userRepository.EditEntity(user);
            await _userRepository.SaveAsync();

        }

        public async Task<EditUserInfoDTO> GetEditUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            return new EditUserInfoDTO
            {
                UserId = userId,
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PostCode = user.PostCode
            };
        }

        public async Task<UserDetailDTO> GetUserDetail(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
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
                MobileActivationNumber = user.MobileActivationNumber
            };
        }

        public async Task<bool> SendActivationSMS(string mobile)
        {
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
            if (user == null) return false;

            user.MobileActivationNumber = new Random().Next(10000, 99999).ToString();
            await _SmsService.SendVerificationSMS(mobile, user.MobileActivationNumber);
            return true;

        }

        public async Task<bool> CheckMobileAuthorization(MobileActivationDTO dto)
        {
            var user = await GetUserByMobile(dto.mobile);
            if (user == null) return false;

            // چون ActivationCode نداری، باید ۵ پارت رو کنار هم بذاری
            var enteredCode = dto.ActivationCodePart1
                            + dto.ActivationCodePart2
                            + dto.ActivationCodePart3
                            + dto.ActivationCodePart4
                            + dto.ActivationCodePart5;

            return enteredCode == user.MobileActivationNumber;
        }




        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().FirstOrDefaultAsync(u => u.MobileNumber == mobile);
        }

        #endregion


    }
}