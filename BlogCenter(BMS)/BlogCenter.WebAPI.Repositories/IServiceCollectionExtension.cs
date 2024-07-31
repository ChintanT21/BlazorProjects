
using BlogCenter.WebAPI.Repositories.Auth;
using BlogCenter.WebAPI.Repositories.Blog;
using BlogCenter.WebAPI.Repositories.BlogCategory;
using BlogCenter.WebAPI.Repositories.Category;
using BlogCenter.WebAPI.Repositories.Generic;
using BlogCenter.WebAPI.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace BlogCenter.WebAPI.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogCategoryRepository,BlogCategoryRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
