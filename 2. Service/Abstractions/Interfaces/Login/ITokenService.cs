using DTOs.Blog.Login;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Login
{
    public interface ITokenService
    {
        Task<TokenResultDto> RequestTokenAsync(ClaimsIdentity identity);
        Task<string> GenerateEncodedToken(ClaimsIdentity identity, double dayNumber, bool isRefreshToken);
        Task<RefreshTokenDto> RefreshToken(string refreshToken);
        bool CheckExpires(string refreshToken);
        ClaimsPrincipal GetPrincipalFromExpiresRefreshToken(string token);
    }
}
