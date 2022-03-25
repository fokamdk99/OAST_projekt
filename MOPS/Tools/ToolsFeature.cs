using Microsoft.Extensions.DependencyInjection;
using MOPS.Tools.Generators;

namespace MOPS.Tools
{
    public static class ToolsFeature
    {
        public static IServiceCollection AddToolsFeature(this IServiceCollection services)
        {
            services.AddSingleton<IEventGenerator, EventGenerator>();
            services.AddSingleton<INumberGenerator, NumberGenerator>();

            return services;
        }
    }
}