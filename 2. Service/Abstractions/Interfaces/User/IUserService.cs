using Common.Runtime.Session;
using DTOs.Blog.User;
using DTOs.Share;
using Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserDto createUserDto);
        Task UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
        Task<CurrentUser> GetUserInfoById(int id);
        Task<string> GetRefreshToken(int id);
        Task SaveRefreshToken(int userId, string refreshToken);
        Task<List<User>> GetAll();
        Task<IPagedResultDto<User>> GetUsers(PagedResultRequestDto pagedResultRequest);
    }
}
