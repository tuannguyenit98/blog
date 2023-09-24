using Abstractions.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Runtime.Session;
using DTOs.Blog.User;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IRepository<User> _userRepository => _unitOfWork.GetRepository<User>();
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var result = await _userRepository
                        .GetAll()
                        .Where(x => x.Id == userId)
                        .FirstOrDefaultAsync();
            return result;
        }

        public async Task<CurrentUser> GetUserInfoById(int id)
        {
            var userFromDb = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userFromDb == null)
            {
                throw new BusinessException("UserIsNotExist");
            }
            return new CurrentUser
            {
                Id = userFromDb.Id,
                UserName = userFromDb.UserName,
            };
        }

        public async Task<string> GetRefreshToken(int userId)
        {
            var refreshToken = await _userRepository
                        .GetAll()
                        .Where(x => x.Id == userId).Select(x => x.RefreshToken).FirstOrDefaultAsync();
            return refreshToken;
        }

        public async Task SaveRefreshToken(int userId, string refreshToken)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id.Equals(userId));
            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _userRepository.InsertAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _userRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == userId);
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;
            user.FullName = updateUserDto.FullName;
            user.UserName = updateUserDto.UserName;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IPagedResultDto<User>> GetUsers(PagedResultRequestDto pagedResultRequest)
        {
            var users = _userRepository.GetAll();
            return await users.GetPagedResultAsync(pagedResultRequest.Page, pagedResultRequest.PageSize, x => x);
        }
    }
}
