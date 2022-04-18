using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public static class EvolutionAlgorithmFeature
    {
        public static IServiceCollection AddEvolutionAlgorithmFeature(this IServiceCollection services)
        {
            services.AddSingleton<IEvolutionAlgorithm, EvolutionAlgorithm>();

            return services;
        }
    }
}