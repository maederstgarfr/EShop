using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Data.DTOs.Account;
using EShop.Data.Entities.Account;

namespace EShop.Application.Services.Interfaces
{
    public interface IPermissionService : IAsyncDisposable
    {
        #region Admin Management
        Task<List<long>> GetAdminPermissions(long userId);
        bool CheckPermission(long permissionId, string mobile);
        Task<CreateAdminDto> GetAdmin(long userId);
        Task RemoveAllUserSelectedRole(long userId);
        Task AddUserToRole(CreateAdminDto dto);
        Task<List<UserRole>> GetAllAdmins();
        #endregion

        #region Role
        Task<List<Role>> GetAllActiveRoles();
        Task<List<Permission>> GetAllPermissions();
        Task RemoveAllRolePermissions(long roleId);
        Task AddRolePermissions(List<long> permissions, long roleId);
        Task CreateRole(CreateRoleDto dto);
        Task<EditRoleDto> GetEditRole(long roleId);
        Task EditRole(EditRoleDto dto);
        Task<bool> DeleteRole(long roleId);
        #endregion
    }
}
}
