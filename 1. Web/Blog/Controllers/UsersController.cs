using Abstractions.Interfaces;
using Common.Helpers;
using DTOs.Blog.User;
using DTOs.Share;
using Entities.Blog;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Produces("application/json")]
    [Route("api/blog/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAll();
            return Ok(ApiResponse<List<User>>.Success(result));
        }

        /// <summary>
        /// Get paging
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> GetUsers([FromQuery] PagedResultRequestDto pagedResultRequestDto)
        {
            if (pagedResultRequestDto.Page == 0)
            {
                var res = await _userService.GetAll();
                return Ok(ApiResponse<List<User>>.Success(res));
            }
            var result = await _userService.GetUsers(pagedResultRequestDto);
            return Ok(ApiResponse<IPagedResultDto<User>>.Success(result));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            await _userService.CreateUserAsync(createUserDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserDto updateUserDto, int id)
        {
            await _userService.UpdateUserAsync(id, updateUserDto);
            return NoContent();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
