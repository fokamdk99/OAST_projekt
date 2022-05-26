using Microsoft.Extensions.DependencyInjection;
using OAST.Tools.Generators;

namespace OAST.Tools
{
    public static class ToolsFeature
    {
        public static IServiceCollection AddToolsFeature(this IServiceCollection services)
        {
            services.AddSingleton<INumberGenerator, NumberGenerator>();
            services.AddSingleton<IStatisticAggregator, StatisticAggregator>();
            services.AddSingleton<ILogs, Logs>();

            return services;
        }
    }
}