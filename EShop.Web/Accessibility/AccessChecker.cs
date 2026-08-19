using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShop.Web.Accessibility
{
    public class AccessChecker
    {
        #region constractor
        private IPermissionService _permissionService;
        private long _permissionId = 0;

        public AccessChecker(long permissionId)
        {
            _permissionId = permissionId;
        }
        #endregion
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var phoneNumber = context.HttpContext.User.Identity.Name;

                if (!_permissionService.CheckPermission(_permissionId, phoneNumber))
                {
                    context.Result = new RedirectResult("/access-denied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/access-denied");
            }
        }
    }
}
