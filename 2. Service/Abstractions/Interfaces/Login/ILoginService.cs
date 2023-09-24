using DTOs.Blog.Login;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Login
{
    public interface ILoginService
    {
        Task<LoginData> LoginAsync(LoginDto dto);
    }
}
