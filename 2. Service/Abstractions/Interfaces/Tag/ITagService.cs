using DTOs.Blog.Tag;
using DTOs.Share;
using Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ITagService
    {
        Task CreateTagAsync(CreateTagDto createTagDto);
        Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto);
        Task DeleteTagAsync(int tagId);
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<List<Tag>> GetAll();
        Task<IPagedResultDto<Tag>> GetTags(PagedResultRequestDto pagedResultRequest);
    }
}
