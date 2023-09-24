using Abstractions.Interfaces;
using Common.Exceptions;
using Common.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class ApiAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserService _userService;

        private IHttpContextAccessor _httpContextAccessor;

        private readonly IUserSession _userSession;

        private string _policy;

        public ApiAuthorizeFilter(
             IUserService userService,
             IHttpContextAccessor httpContextAccessor
            , IUserSession userSession
            , string policy = "")
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;
            _policy = policy;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isAllowAnonymous = ApiAuthorizeHelper.IsAllowAnonymous(context);
            if (isAllowAnonymous)
            {
                return;
            }

            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
            try
            {
                var userId = _userSession.UserId;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new BusinessException("UserIsNotExist");
                }
                var userInfo = await _userService.GetUserInfoById(Convert.ToInt32(userId));
                if (userInfo is null)
                {
                    throw new BusinessException("UserIsNotExist");
                }
                CurrentUser.Current = userInfo;
                if (string.IsNullOrWhiteSpace(_policy))
                {
                    return;
                }
            }
            catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }
        }
    }
}
