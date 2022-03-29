using Microsoft.Extensions.DependencyInjection;

namespace OAST.Server
{
    public static class ServerFeature
    {
        public static IServiceCollection AddServerFeature(this IServiceCollection services)
        {
            services.AddSingleton<ICustomServer, CustomServer>();
            services.AddSingleton<IServerMeasurements, ServerMeasurements>();

            return services;
        }
    }
}