using Microsoft.Extensions.DependencyInjection;

namespace MOPS.Queue
{
    public static class QueueFeature
    {
        public static IServiceCollection AddQueueFeature(this IServiceCollection services)
        {
            services.AddSingleton<ICustomQueue, CustomQueue>();

            return services;
        }
    }
}