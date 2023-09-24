using Abstractions.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Extentions;
using Common.Runtime.Session;
using DTOs.Blog.Tag;
using DTOs.Share;
using Entities.Blog;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Implementations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<Tag> _tagRepository => _unitOfWork.GetRepository<Tag>();
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            var result = await _tagRepository
                        .GetAll()
                        .Where(x => x.Id == tagId)
                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateTagAsync(CreateTagDto createTagDto)
        {
            var tag = _mapper.Map<Tag>(createTagDto);
            await _tagRepository.InsertAsync(tag);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Tag>> GetAll()
        {
            var users = await _tagRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto)
        {
            var tag = await _tagRepository.GetAll().FirstOrDefaultAsync(x => x.Id == tagId);
            tag.Title = updateTagDto.Title;
            tag.MetaTitle = updateTagDto.MetaTitle;
            tag.Slug = updateTagDto.Title.Slugify();
            await _tagRepository.UpdateAsync(tag);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteTagAsync(int tagId)
        {
            await _tagRepository.DeleteAsync(tagId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IPagedResultDto<Tag>> GetTags(PagedResultRequestDto pagedResultRequest)
        {
            var categories = _tagRepository.GetAll();
            return await categories.GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => x);
        }
    }
}
