using Abstractions.Interfaces.Login;
using Common.Helpers;
using Common.Runtime.Security;
using Common.Unknown;
using DTOs.Blog.Login;
using Entities.Blog;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services.Implementations.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<User> _userRepository => _unitOfWork.GetRepository<User>();

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginData> LoginAsync(LoginDto dto)
        {
            var userToVerify = await GetUserByUserNameAsync(dto.UserName);

            if (userToVerify == null)
            {
                return new LoginData(LoginResultType.InvalidUserNameOrPassword);
            }

            if (!LoginHelper.CheckPassword(dto.Password, userToVerify.Password))
            {
                return new LoginData(LoginResultType.InvalidUserNameOrPassword);
            }

            var claimIdentity = GenerateClaimsIdentity(userToVerify);
            return new LoginData(claimIdentity);
        }

        private async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName && x.DeleteAt == null);
        }

        private ClaimsIdentity GenerateClaimsIdentity(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(GlotechClaimTypes.Id, user.Id.ToString()),
                new Claim(GlotechClaimTypes.UserName, user.UserName),
            };

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);
        }
    }
}
