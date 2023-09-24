using Abstractions.Interfaces;
using AutoMapper;
using Common.Extentions;
using DTOs.Blog.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<Category> _categoryRepository => _unitOfWork.GetRepository<Category>();
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var result = await _categoryRepository
                        .GetAll()
                        .Where(x => x.Id == categoryId)
                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _categoryRepository.InsertAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            var users = await _categoryRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == categoryId);
            category.Title = updateCategoryDto.Title;
            category.MetaTitle = updateCategoryDto.MetaTitle;
            category.ParentId = updateCategoryDto.ParentId;
            await _categoryRepository.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IPagedResultDto<Category>> GetCategories(PagedResultRequestDto pagedResultRequest)
        {
            var categories = _categoryRepository.GetAll();
            return await categories.GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => x);
        }
    }
}
