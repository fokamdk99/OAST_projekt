using Microsoft.Extensions.DependencyInjection;

namespace OAST.Simulator
{
    public static class SimulatorFeature
    {
        public static IServiceCollection AddSimulatorFeature(this IServiceCollection services)
        {
            services.AddSingleton<ISimulator, Simulator>();

            return services;
        }
    }
}