using DTOs.Blog.Category;
using DTOs.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(int categoryId, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int categoryId);
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<List<CategoryDto>> GetAll();
        Task<IPagedResultDto<CategoryDto>> GetCategories(PagedResultRequestDto pagedResultRequest, string searchTerm);
    }
}
