using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadImages.BLL.Services;
using ReadImages.BLL.Contracts;

namespace ReadImages.API.Ioc
{
    public static class DependencyConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHandleFilesService, HandleFilesService>();
            services.AddScoped<IFileStoreService, FileStore>();
        }
    }
}
