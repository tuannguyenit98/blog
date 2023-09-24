using DTOs.Blog.Comment;
using DTOs.Share;
using Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto);
        Task DeleteCommentAsync(int commentId);
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<List<Comment>> GetAll();
        Task<IPagedResultDto<Comment>> GetComments(PagedResultRequestDto pagedResultRequest);
    }
}
