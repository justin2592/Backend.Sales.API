using Backend.Sales.Application.Interface;
using Backend.Sales.Application.Services;

namespace Backend.Sales.Application.Installers
{
    public static class ApplicationServiceInstaller
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IApiKeyService, ApiKeyService>();
            services.AddLogging(configure => configure.AddConsole());
            return services;
        }

    }
}
