using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.BruteForceTools
{
    public static class BfToolsFeature
    {
        public static IServiceCollection AddBfToolsFeature(this IServiceCollection services)
        {
            services.AddSingleton<IBfTools, BfTools>();

            return services;
        }
    }
}