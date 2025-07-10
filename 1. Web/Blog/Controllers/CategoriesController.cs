using Abstractions.Interfaces;
using Common.Helpers;
using DTOs.Blog.Category;
using DTOs.Share;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Produces("application/json")]
    [Route("api/blog/categories")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get paging
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ModelValidationFilter]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories([FromQuery] PagedResultRequestDto pagedResultRequestDto, [FromQuery] string searchTerm)
        {
            if (pagedResultRequestDto.Page == 0)
            {
                var res = await _categoryService.GetAll();
                return Ok(ApiResponse<List<CategoryDto>>.Success(res));
            }
            var result = await _categoryService.GetCategories(pagedResultRequestDto, searchTerm);
            return Ok(ApiResponse<IPagedResultDto<CategoryDto>>.Success(result));
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
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(ApiResponse<CategoryDto>.Success(result));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="createCategoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="updateCategoryDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ModelValidationFilter]
        [TypeFilter(typeof(ApiAuthorizeFilter))]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto, int id)
        {
            await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
