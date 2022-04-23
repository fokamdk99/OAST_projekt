using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.History
{
    public static class HistoryFeature
    {
        public static IServiceCollection AddHistoryFeature(this IServiceCollection services)
        {
            services.AddSingleton<IHistory, OptimalizationHistory>();
            
            return services;
        }
    }
}