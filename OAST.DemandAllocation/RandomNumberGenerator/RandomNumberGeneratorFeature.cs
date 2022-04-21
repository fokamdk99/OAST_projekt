using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.RandomNumberGenerator
{
    public static class RandomNumberGeneratorFeature
    {
        public static IServiceCollection AddRandomNumberGeneratorFeature(this IServiceCollection services)
        {
            services.AddSingleton<IRandomNumberGenerator, RandomGenerator>();
            
            return services;
        }
    }
}