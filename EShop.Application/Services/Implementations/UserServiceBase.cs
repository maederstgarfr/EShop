namespace EShop.Application.Services.Implementations
{
    public class UserServiceBase
    {

        public Task<bool> CheckUserExistByMobile(string mobile)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Register Methods
        public async Task<RegisterOrLoginResult> RegisterOrLoginUser(RegisterUserDTO dto)
        {
            var checkUser = await CheckUserExistByMobile(dto.MobileNumber);

        }

        #endregion



        #region Register Methods
        public async Task<RegisterOrLoginResult> RegisterOrLoginUser(RegisterUserDTO dto)
        {
            var checkUser = await CheckUserExistByMobile(dto.MobileNumber);

        }
    }
}