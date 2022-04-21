using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public static class EvolutionAlgorithmFeature
    {
        public static IServiceCollection AddEvolutionAlgorithmFeature(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IEvolutionAlgorithm<>), typeof(EvolutionAlgorithm<>));

            return services;
        }
    }
}