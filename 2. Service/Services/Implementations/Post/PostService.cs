using Abstractions.Interfaces;
using AutoMapper;
using DTOs.Blog.Post;
using DTOs.Share;
using Entities.Blog;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Implementations.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<Post> _postRepository => _unitOfWork.GetRepository<Post>();
        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            var result = await _postRepository
                        .GetAll()
                        .Where(x => x.Id == postId)
                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task CreatePostAsync(CreatePostDto createPostDto)
        {
            var Post = _mapper.Map<Post>(createPostDto);
            await _postRepository.InsertAsync(Post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Post>> GetAll()
        {
            var users = await _postRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task UpdatePostAsync(int postId, UpdatePostDto updatePostDto)
        {
            var post = await _postRepository.GetAll().FirstOrDefaultAsync(x => x.Id == postId);
            post.Title = updatePostDto.Title;
            post.MetaTitle = updatePostDto.MetaTitle;
            post.Status = updatePostDto.Status;
            post.Content = updatePostDto.Content;
            post.Image = updatePostDto.Image;
            post.FK_CategoryId = updatePostDto.FK_CategoryId;
            post.FK_UserId = updatePostDto.FK_UserId;
            await _postRepository.UpdateAsync(post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            await _postRepository.DeleteAsync(postId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IPagedResultDto<Post>> GetPosts(PagedResultRequestDto pagedResultRequest)
        {
            var categories = _postRepository.GetAll();
            return await categories.GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => x);
        }

        public async Task<List<Post>> GetPostsByCategoryIdAsync(int categoryId)
        {
           var posts = await _postRepository.GetAll().Where(x => x.FK_CategoryId == categoryId).ToListAsync();
            return posts;
        }
    }
}
