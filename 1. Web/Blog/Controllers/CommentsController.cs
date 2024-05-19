using Abstractions.Interfaces;
using Common.Helpers;
using DTOs.Blog.Comment;
using DTOs.Share;
using Entities.Blog;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Produces("application/json")]
    [Route("api/blog/comments")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> GetComments()
        {
            var result = await _commentService.GetAll();
            return Ok(ApiResponse<List<Comment>>.Success(result));
        }

        /// <summary>
        /// Get paging
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> GetPagingComments([FromQuery] PagedResultRequestDto pagedResultRequestDto)
        {
            var result = await _commentService.GetComments(pagedResultRequestDto);
            return Ok(ApiResponse<IPagedResultDto<Comment>>.Success(result));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="createCommentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateComment([FromBody] CreateOrUpdateCommentDto createCommentDto)
        {
            await _commentService.CreateCommentAsync(createCommentDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="updateCommentDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateComment([FromQuery] CreateOrUpdateCommentDto updateCommentDto, int id)
        {
            await _commentService.UpdateCommentAsync(id, updateCommentDto);
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
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
