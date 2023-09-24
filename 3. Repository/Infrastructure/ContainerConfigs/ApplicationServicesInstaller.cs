using Abstractions.Interfaces;
using Abstractions.Interfaces.Login;
using Common.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Implementations;
using Services.Implementations.Login;

namespace Infrastructure.ContainerConfigs
{
    public static class ApplicationServicesInstaller
    {
        public static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
