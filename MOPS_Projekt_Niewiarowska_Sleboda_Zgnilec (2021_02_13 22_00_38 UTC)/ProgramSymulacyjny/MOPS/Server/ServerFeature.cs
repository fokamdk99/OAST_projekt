using Microsoft.Extensions.DependencyInjection;

namespace MOPS.Server
{
    public static class ServerFeature
    {
        public static IServiceCollection AddServerFeature(this IServiceCollection services)
        {
            services.AddSingleton<ICustomServer, CustomServer>();

            return services;
        }
    }
}