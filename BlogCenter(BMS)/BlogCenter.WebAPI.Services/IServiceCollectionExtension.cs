
using BlogCenter.WebAPI.Repositories.Auth;
using BlogCenter.WebAPI.Services.Auth;
using BlogCenter.WebAPI.Services.Blog;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCenter.WebAPI.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogService, BlogService>();
            return services;
        }
    }
}
