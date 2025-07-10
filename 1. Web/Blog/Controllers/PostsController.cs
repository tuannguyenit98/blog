using Abstractions.Interfaces;
using Common.Helpers;
using DTOs.Blog.Post;
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
    [Route("api/blog/posts")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Get paging
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingPosts([FromQuery] PagedResultRequestDto pagedResultRequestDto, [FromQuery] string searchTerm)
        {
            if (pagedResultRequestDto.Page == 0)
            {
                var res = await _postService.GetAll();
                return Ok(ApiResponse<List<Post>>.Success(res));
            }
            var result = await _postService.GetPosts(pagedResultRequestDto, searchTerm);
            return Ok(ApiResponse<IPagedResultDto<PostDto>>.Success(result));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _postService.GetPostByIdAsync(id);
            return Ok(ApiResponse<Post>.Success(result));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="createPostDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            await _postService.CreatePostAsync(createPostDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="updatePostDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostDto updatePostDto, int id)
        {
            await _postService.UpdatePostAsync(id, updatePostDto);
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
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get posts features
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("featured")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostFeatures()
        {
            var result = await _postService.GetPostFeaturesAsync();
            return Ok(ApiResponse<List<PostDto>>.Success(result));
        }

        /// <summary>
        /// Get post recent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("recent")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostRecent()
        {
            var result = await _postService.GetPostRecentAsync();
            return Ok(ApiResponse<PostDto>.Success(result));
        }

        /// <summary>
        /// Get By Slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}/detail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await _postService.GetPostBySlugAsync(slug);
            return Ok(ApiResponse<PostDetailDto>.Success(result));
        }

        [HttpGet("{slug}/category")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostsByCategorySlug([FromQuery] PagedResultRequestDto pagedResultRequestDto, string slug)
        {
            var result = await _postService.GetPostsByCategorySlugAsync(pagedResultRequestDto, slug);
            return Ok(ApiResponse<IPagedResultDto<PostDto>>.Success(result));
        }
    }
}
