using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace Common.Runtime.Security
{
    public static class ClaimsIdentityExtensions
    {

        public static int GetId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userIdClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.Id);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                throw new AuthenticationException();
            }

            int id;
            if (!int.TryParse(userIdClaim.Value, out id))
            {
                throw new AuthenticationException();
            }

            return id;
        }

        public static string GetName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userNameClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserName);
            if (string.IsNullOrEmpty(userNameClaim?.Value))
            {
                throw new AuthenticationException();
            }

            return userNameClaim.Value;
        }
    }
}
