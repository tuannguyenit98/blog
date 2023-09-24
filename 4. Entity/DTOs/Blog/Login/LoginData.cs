using Common.Unknown;
using System.Security.Claims;

namespace DTOs.Blog.Login
{
    public class LoginData
    {
        public LoginResultType Result { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public LoginData(LoginResultType result)
        {
            Result = result;
        }

        public LoginData(ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            Identity = identity;
        }
    }
}
