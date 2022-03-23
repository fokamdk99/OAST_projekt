using Microsoft.Extensions.DependencyInjection;

namespace MOPS.Events
{
    public static class EventFeature
    {
        public static IServiceCollection AddEventFeature(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandler, EventHandler>();

            return services;
        }
    }
}