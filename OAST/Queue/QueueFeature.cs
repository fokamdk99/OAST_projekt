using Microsoft.Extensions.DependencyInjection;

namespace OAST.Queue
{
    public static class QueueFeature
    {
        public static IServiceCollection AddQueueFeature(this IServiceCollection services)
        {
            services.AddSingleton<ICustomQueue, CustomQueue>();
            services.AddSingleton<IQueueMeasurements, QueueMeasurements>();

            return services;
        }
    }
}