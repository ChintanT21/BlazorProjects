using BlogCenter.Services.Category;
using BlogCenter.Services.User;
using BlogCenter.WebAPI.Repositories.Category;

namespace BlogCenter.Blazor.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddClientService(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICategoryClientService, CategoryClientService>();
            services.AddScoped<IUserClientService, UserClientService>();
            return services;
        }
    }
}
