using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.BruteForceAlgorithm
{
    public static class BruteForceAlgorithmFeature
    {
        public static IServiceCollection AddBruteForceAlgorithmFeature(this IServiceCollection services)
        {
            services.AddSingleton<IBruteForceAlgorithm, BruteForceAlgorithm>();

            return services;
        }
    }
}