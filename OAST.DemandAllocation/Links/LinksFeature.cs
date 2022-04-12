using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.Links
{
    public static class LinksFeature
    {
        public static IServiceCollection AddLinksFeature(this IServiceCollection services)
        {
            services.AddSingleton<ILink, Link>();

            return services;
        }
    }
}