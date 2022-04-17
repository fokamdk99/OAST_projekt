using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.EvolutionTools
{
    public static class EvolutionToolsFeature
    {
        public static IServiceCollection AddEvolutionToolsFeature(this IServiceCollection services)
        {
            services.AddSingleton<IInheritance, Inheritance>();
            services.AddSingleton<IReproduction, Reproduction>();
            services.AddSingleton<ITools, Tools>();

            return services;
        }
    }
}