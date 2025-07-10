using Abstractions.Interfaces;
using Abstractions.Interfaces.Login;
using Blog.Controllers;
using Common.Helpers;
using Common.Runtime.Security;
using Common.Unknown;
using DTOs.Blog.Login;
using Infrastructure.ApiControllers;
using Infrastructure.ApiResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace Blog.UnitTest.Controllers
{
    public class LoginControllerTest : ApiControllerBase
    {
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<ILoginService> _loginServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly LoginController _loginController;
        private readonly Mock<IResponseCookies> _responseCookiesMock;
        public LoginControllerTest()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _loginServiceMock = new Mock<ILoginService>();
            _userServiceMock = new Mock<IUserService>();

            // Setup HttpContext and Response for cookie handling
            _responseCookiesMock = new Mock<IResponseCookies>();

            _loginController = new LoginController(_tokenServiceMock.Object, _loginServiceMock.Object, _userServiceMock.Object);
        }
        [Fact]
        public async void Login_ReturnsBadRequest_WhenInvalidUsernameOrPassword()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = "testuser",
                Password = "password123"
            };
            var loginResult = new LoginData(LoginResultType.InvalidUserNameOrPassword);

            _loginServiceMock.Setup(service => service.LoginAsync(loginDto)).ReturnsAsync(loginResult);

            // Act
            var result = await _loginController.Login(loginDto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var apiResponse = Assert.IsType<ApiErrorResult>(badRequest.Value);
            Assert.Equal("UserLoginInvalidUserNameOrPassword", apiResponse.ErrorCode);
        }
        [Fact]
        public async void Login_ReturnsOk_WhenLoginSuccessful()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                UserName = "testuser",
                Password = "password123"
            };
            var identityMock = new ClaimsIdentity();

            var userClaims = new List<Claim>
            {
                new Claim(GlotechClaimTypes.Id, "1"),
            };

            identityMock.AddClaims(userClaims);

            var loginResult = new LoginData(identityMock);

            var tokenResultDto = new TokenResultDto
            {
                Id = 1,
                AccessToken = "jwt_token",
            };

            var refreshToken = "refresh_token";

            // Set up the mock service
            _loginServiceMock.Setup(service => service.LoginAsync(loginDto)).ReturnsAsync(loginResult);
            _tokenServiceMock.Setup(service => service.RequestTokenAsync(loginResult.Identity)).ReturnsAsync(tokenResultDto);
            _tokenServiceMock.Setup(service => service.GenerateEncodedToken(loginResult.Identity, 1, true)).ReturnsAsync(refreshToken);
            _userServiceMock.Setup(service => service.SaveRefreshToken(It.IsAny<int>(), refreshToken)).Returns(Task.CompletedTask);

            var responseMock = new Mock<HttpResponse>();
            responseMock.Setup(r => r.Cookies).Returns(_responseCookiesMock.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Response).Returns(responseMock.Object);


            _loginController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object,
            };

            // Act
            var result = await _loginController.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponseSucess<TokenResultDto>>(okResult.Value);
            Assert.Equal("jwt_token", apiResponse.data.AccessToken);

            // Verify cookies
            _responseCookiesMock.Verify(x => x.Append("_refreshToken", "refresh_token", It.Is<CookieOptions>(o => o.HttpOnly && o.Secure && o.SameSite == SameSiteMode.None)), Times.Once);
        }
    }
}
