using Common.Runtime.Security;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Common.Runtime.Session
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserEmail()
        {
            var userEmailClaim = Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmailClaim?.Value))
            {
                return null;
            }

            int userId;
            if (!int.TryParse(userEmailClaim.Value, out userId))
            {
                return null;
            }

            return userId;
        }

        public string UserName
        {
            get { return Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserName)?.Value; }
        }

        public string UserId
        {
            get { return Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.Id)?.Value; }
        }
    }
}
