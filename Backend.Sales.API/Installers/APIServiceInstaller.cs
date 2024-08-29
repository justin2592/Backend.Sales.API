using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Backend.Sales.Api.Installers
{
    public static class APIServiceInstaller
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddControllers();
            return services;
        }
    }
}
