using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.Demands
{
    public static class DemandsFeature
    {
        public static IServiceCollection AddDemandsFeature(this IServiceCollection services)
        {
            services.AddSingleton<IDemand, Demand>();
            services.AddSingleton<IDemandPath, DemandPath>();

            return services;
        }
    }
}