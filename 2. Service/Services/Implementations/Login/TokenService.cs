using Abstractions.Interfaces;
using Abstractions.Interfaces.Login;
using Common.Helpers;
using Common.Runtime.Security;
using DTOs.Blog.Login;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Login
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService _userService;

        public TokenService(IOptions<JwtIssuerOptions> jwtOptions, IUserService userService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<TokenResultDto> RequestTokenAsync(ClaimsIdentity identity)
        {
            return new TokenResultDto
            {
                Id = identity.GetId(),
                TokenType = "Bearer",
                AccessToken = await GenerateEncodedToken(identity, 0.0416666667, false),//token expires 1h
                UserName = identity.GetName(),
            };
        }

        public async Task<string> GenerateEncodedToken(ClaimsIdentity identity, double dayNumber, bool isRefreshToken)
        {
            _jwtOptions.IssuedAt = DateTime.Now;

            var claims = identity.Claims.Union(
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(
                        JwtRegisteredClaimNames.Iat,
                        _jwtOptions.IssuedAt.LocalToUtcTime().ToSecondsTimestamp().ToString(),
                        ClaimValueTypes.Integer64),
                });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(isRefreshToken ? _jwtOptions.SecretKeyRefresh : _jwtOptions.SecretKeyAccess));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.IssuedAt,
                _jwtOptions.IssuedAt.AddDays(dayNumber),
                signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsPrincipal GetPrincipalFromExpiresRefreshToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKeyRefresh)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }

        public async Task<RefreshTokenDto> RefreshToken(string refreshToken)
        {
            var tokenData = new RefreshTokenDto();
            var token = new JwtSecurityToken(refreshToken);
            var userId = int.Parse(token.Claims.First(x => x.Type == GlotechClaimTypes.Id).Value);
            var user = await _userService.GetUserByIdAsync(userId);

            var userClaims = new List<Claim>
            {
                new Claim(GlotechClaimTypes.Id, user.Id.ToString()),
                new Claim(GlotechClaimTypes.UserName, user.UserName),
            };

            var identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);

            tokenData.Token = await GenerateEncodedToken(identity, 0.0416666667, false);//token expires 1h
            tokenData.RefreshToken = await GenerateEncodedToken(identity, 1, true);//refreshToken expires 1day
            return tokenData;
        }

        public bool CheckExpires(string refreshToken)
        {
            int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var token = new JwtSecurityToken(jwtEncodedString: refreshToken);
            if (token.Payload.Exp < unixTimestamp)
            {
                return true;
            }
            return false;
        }
    }
}
