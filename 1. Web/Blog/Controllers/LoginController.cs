﻿using Common.Helpers;
using Common.Resources;
using Common.Runtime.Session;
using Common.Unknown;
using DTOs.Blog.Login;
using Infrastructure.ApiControllers;
using Infrastructure.ApiResults;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Common.Runtime.Security;
using Abstractions.Interfaces.Login;
using Abstractions.Interfaces;

namespace Blog.Controllers
{
    [Produces("application/json")]
    [Route("api/blog")]
    [Authorize]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public LoginController(
            ITokenService tokenService
            , ILoginService loginService
            , IUserService userService
            )
        {
            _tokenService = tokenService;
            _loginService = loginService;
            _userService = userService;
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ModelValidationFilter]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginResult = await _loginService.LoginAsync(dto);
            switch (loginResult.Result)
            {
                case LoginResultType.InvalidUserNameOrPassword:
                    return BadRequest(ApiErrorCodes.UserLoginInvalidUserNameOrPassword, ExceptionResource.InvalidUsernameOrPassword);

                case LoginResultType.Success:
                    var jwtToken = await _tokenService.RequestTokenAsync(loginResult.Identity);
                    var refreshToken = await _tokenService.GenerateEncodedToken(loginResult.Identity, 1, true);
                    await _userService.SaveRefreshToken(loginResult.Identity.GetId(), refreshToken);
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    };
                    Response.Cookies.Append("_refreshToken", refreshToken, cookieOptions);
                    return Ok(ApiResponse<TokenResultDto>.Success(jwtToken));

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["_refreshToken"];
            var principal = _tokenService.GetPrincipalFromExpiresRefreshToken(refreshToken);
            var refreshTokenUser = await _userService.GetRefreshToken(principal.Identity.GetId());
            if (refreshToken != null && refreshToken == refreshTokenUser)
            {
                var isExpires = _tokenService.CheckExpires(refreshToken);
                if (isExpires)
                {
                    Response.Cookies.Delete("_refreshToken");
                    return StatusCode(201);
                }
                var tokenData = await _tokenService.RefreshToken(refreshToken);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                };
                await _userService.SaveRefreshToken(principal.Identity.GetId(), tokenData.RefreshToken);
                Response.Cookies.Append("_refreshToken", tokenData.RefreshToken, cookieOptions);
                return Ok(ApiResponse<string>.Success(tokenData.Token));
            }
            else
            {
                return StatusCode(201);
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("me/logout")]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> Logout()
        {
            await _userService.SaveRefreshToken(CurrentUser.Current.Id, null);
            Response.Cookies.Delete("_refreshToken");
            return StatusCode(201);
        }
    }
}
