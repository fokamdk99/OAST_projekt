using Microsoft.Extensions.DependencyInjection;

namespace MOPS.Simulator
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