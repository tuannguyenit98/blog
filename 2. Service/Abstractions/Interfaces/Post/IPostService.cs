using DTOs.Blog;
using DTOs.Blog.Post;
using DTOs.Share;
using Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface IPostService
    {
        Task CreatePostAsync(CreatePostDto createPostDto);
        Task UpdatePostAsync(int postId, UpdatePostDto updatePostDto);
        Task DeletePostAsync(int postId);
        Task<Post> GetPostByIdAsync(int postId);
        Task<List<Post>> GetPostsByCategoryIdAsync(int categoryId);
        Task<List<Post>> GetAll();
        Task<IPagedResultDto<PostDto>> GetPosts(PagedResultRequestDto pagedResultRequest);
        Task<FeaturePostDto> GetPostFeaturesAsync();
    }
}
