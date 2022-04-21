using Microsoft.Extensions.DependencyInjection;
using OAST.DemandAllocation.BruteForceAlgorithm;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Output;
using OAST.DemandAllocation.RandomNumberGenerator;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation
{
    public static class DemandAllocationFeature
    {
        public static IServiceCollection AddDemandAllocationFeature(this IServiceCollection services)
        {

            services
                .AddBruteForceAlgorithmFeature()
                .AddBfToolsFeature()
                .AddDemandsFeature()
                .AddEvolutionAlgorithmFeature()
                .AddEvolutionToolsFeature()
                .AddFileReaderFeature()
                .AddLinksFeature()
                .AddOutputFeature()
                .AddRandomNumberGeneratorFeature()
                .AddTopologyFeature();
            
            return services;
        }
    }
}