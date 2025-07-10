using Common.Helpers;
using DTOs.Blog.Login;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Blog.IntegrationTest.Controllers
{
    public class LoginAPITest : TestFixture
    {
        [Fact]
        public async Task Login_ReturnsOk_WhenLoginSuccessful()
        {
            // Arrange
            var loginData = new LoginDto
            {
                UserName = "Admin",
                Password = "123456",
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/blog/login", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
            var body = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<ApiResponseSucess<TokenResultDto>>(body);
            Assert.False(string.IsNullOrEmpty(json.data.AccessToken));

            // Check cookie
            var setCookie = response.Headers.SingleOrDefault(h => h.Key == "Set-Cookie").Value?.FirstOrDefault();
            Assert.Contains("_refreshToken", setCookie);
        }
    }
}
