using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.Output
{
    public static class OutputFeature
    {
        public static IServiceCollection AddOutputFeature(this IServiceCollection services)
        {
            services.AddSingleton<IOutputSaver, OutputSaver>();
            
            return services;
        }
    }
}