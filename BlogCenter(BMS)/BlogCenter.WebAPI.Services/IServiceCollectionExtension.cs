
using BlogCenter.WebAPI.Repositories.Auth;
using BlogCenter.WebAPI.Repositories.Base;
using BlogCenter.WebAPI.Repositories.Generic;
using BlogCenter.WebAPI.Services.Auth;
using BlogCenter.WebAPI.Services.Blog;
using BlogCenter.WebAPI.Services.BlogCategory;
using BlogCenter.WebAPI.Services.Category;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCenter.WebAPI.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            return services;
        }
    }
}
