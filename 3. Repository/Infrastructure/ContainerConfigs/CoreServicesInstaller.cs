using AutoMapper;
using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Filters;
using Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IValidator = DTOs.Validators.IValidator;

namespace Infrastructure.ContainerConfigs
{
    public static class CoreServicesInstaller
    {
        public static void ConfigureCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder
                       .SetIsOriginAllowed(host => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            services.AddAutoMapper(typeof(UserProfile));

            services.AddMvcCore(
                options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                });
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(IValidator));

            services.AddDbContext<BlogDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork<BlogDbContext>>();
            services.AddScoped<IRepositoryFactory, UnitOfWork<BlogDbContext>>();
            services.AddScoped<ModelValidationFilterAttribute>();
            services.AddMemoryCache();
        }
    }
}
