using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.Fitness
{
    public static class FitnessFeature
    {
        public static IServiceCollection AddFitnessFeature(this IServiceCollection services, bool isDap)
        {
            if (isDap)
            {
                services.AddSingleton<IFitnessFunction, DapFitnessFunction>();
                return services;
            }
            
            services.AddSingleton<IFitnessFunction, DdapFitnessFunction>();

            return services;
        }
    }
}