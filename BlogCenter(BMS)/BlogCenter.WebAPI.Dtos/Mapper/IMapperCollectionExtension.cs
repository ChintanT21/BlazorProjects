using Microsoft.Extensions.DependencyInjection;

namespace BlogCenter.WebAPI.Repositories
{
    public static class IMapperCollectionExtension
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {

            return services;
        }
    }
}
