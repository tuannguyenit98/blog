using DTOs.Blog.Category;
using DTOs.Share;
using Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(int categoryId, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int categoryId);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<List<CategoryDto>> GetAll();
        Task<IPagedResultDto<Category>> GetCategories(PagedResultRequestDto pagedResultRequest, string searchTerm);
    }
}
