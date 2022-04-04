using Microsoft.Extensions.DependencyInjection;
using OAST.Tools.Generators;

namespace OAST.Tools
{
    public static class ToolsFeature
    {
        public static IServiceCollection AddToolsFeature(this IServiceCollection services)
        {
            services.AddSingleton<IEventGenerator, EventGenerator>();
            services.AddSingleton<INumberGenerator, NumberGenerator>();
            services.AddSingleton<IStatistic, Statistic>();
            services.AddSingleton<ILogs, Logs>();

            return services;
        }
    }
}