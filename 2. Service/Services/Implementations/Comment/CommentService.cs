using Abstractions.Interfaces;
using AutoMapper;
using Common.Runtime.Session;
using DTOs.Blog.Comment;
using DTOs.Share;
using Entities.Blog;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Services.Implementations.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<Comment> _commentRepository => _unitOfWork.GetRepository<Comment>();
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            var result = await _commentRepository
                        .GetAll()
                        .Where(x => x.Id == commentId)
                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.FK_UserId = CurrentUser.Current.Id;
            await _commentRepository.InsertAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Comment>> GetAll()
        {
            var users = await _commentRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == commentId);
            comment.FK_UserId = CurrentUser.Current.Id;
            comment.ParentId = updateCommentDto.ParentId;
            comment.FK_PostId = updateCommentDto.FK_PostId;
            comment.Content = updateCommentDto.Content;
            await _commentRepository.UpdateAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IPagedResultDto<Comment>> GetComments(PagedResultRequestDto pagedResultRequest)
        {
            var categories = _commentRepository.GetAll();
            return await categories.GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => x);
        }
    }
}
