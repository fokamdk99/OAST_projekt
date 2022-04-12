using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.Topology
{
    public static class TopologyFeature
    {
        public static IServiceCollection AddTopologyFeature(this IServiceCollection services)
        {
            services.AddSingleton<ITopology, Topology>();

            return services;
        }
    }
}