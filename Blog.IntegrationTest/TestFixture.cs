using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;

namespace Blog.IntegrationTest
{
    public class TestFixture : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly TestServer _server; // run app in memory
        public TestFixture()
        {
            // Lấy đường dẫn thư mục hiện tại của test project `.../bin/Debug/net8.0/` 
            var integrationTestsPath = PlatformServices.Default.Application.ApplicationBasePath;

            // thư mục chứa mã của project chính
            var applicationPath = Path.GetFullPath(Path.Combine(integrationTestsPath, "../../../../1. Web/Blog"));

            // giống như chạy `dotnet run` nhưng trong bộ nhớ
            _server = new TestServer(WebHost.CreateDefaultBuilder() // khởi tạo cấu hình server ASP.NET Core mặc định (có `appsettings`, logging, v.v.) 
                .UseStartup<Startup>() // sử dụng `Startup.cs` từ project chính
                .UseContentRoot(applicationPath) // cho biết thư mục gốc của project
                .UseEnvironment("Development"));

            _httpClient = _server.CreateClient();
        }
        public void Dispose()
        {
            _httpClient.Dispose();
            _server.Dispose();
        }
    }
}
