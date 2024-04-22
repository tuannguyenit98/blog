using Abstractions.Interfaces;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Exceptions;
using Common.Helpers;
using Common.Runtime.Security;
using Common.Runtime.Session;
using DTOs.Blog.Post;
using DTOs.Share;
using Entities.Blog;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.Implementations.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        private IRepository<Post> _postRepository => _unitOfWork.GetRepository<Post>();
        public PostService(
            IUnitOfWork unitOfWork
            , IMapper mapper
            , IOptions<CloudinarySettings> cloudinarySettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Account account = new Account(cloudinarySettings.Value.CloudName,
                cloudinarySettings.Value.ApiKey,
                cloudinarySettings.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
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
            var post = _mapper.Map<Post>(createPostDto);
            post.FK_UserId = CurrentUser.Current.Id;
            post.Image = await AddPhotoAsyc(createPostDto.File);
            await _postRepository.InsertAsync(post);
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
            string photoUrl = post.Image;
            post.Title = updatePostDto.Title;
            post.MetaTitle = updatePostDto.MetaTitle;
            post.Status = updatePostDto.Status;
            post.Content = updatePostDto.Content;
            post.Image = await AddPhotoAsyc(updatePostDto.File);
            post.FK_CategoryId = updatePostDto.FK_CategoryId;
            post.FK_UserId = CurrentUser.Current.Id;
            await _postRepository.UpdateAsync(post);
            await DeletePhotoAsyc(ImageHelper.GetPublicIdFromUrl(photoUrl));
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

        private async Task<string> UploadFile(IFormFile file)
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");// all file phải nằm trong wwwroot
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }
            var extension = Path.GetExtension(file.FileName).ToLower();

            var allowEts = new List<string>() { ".jpg", ".png", ".doc", ".jpeg" };

            if (!allowEts.Contains(extension))
            {
                throw new BusinessException("Invalid file");
            }

            var path = Path.Combine(pathBuilt, file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "http://localhost:55288/uploads/" + file.FileName;
        }

        private async Task<string> AddPhotoAsyc(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)                                        
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new CloudinaryDotNet.FileDescription(file.Name, stream)
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return uploadResult.Uri.ToString();
        }

        private async Task DeletePhotoAsyc(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
