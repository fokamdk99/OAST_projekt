using Microsoft.Extensions.DependencyInjection;

namespace OAST.DemandAllocation.FileReader
{
    public static class FileReaderFeature
    {
        public static IServiceCollection AddFileReaderFeature(this IServiceCollection services)
        {
            services.AddSingleton<IFileReader, FileReader>();

            return services;
        }
    }
}