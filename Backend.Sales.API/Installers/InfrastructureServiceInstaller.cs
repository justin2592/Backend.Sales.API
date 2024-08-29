using Backend.Domain.Interface;
using Backend.Sales.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.Sales.Application.Installers
{
    public static class InfrastructureServiceInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ISaleDbContext, SaleDbContext> (options =>
                options.UseSqlServer(configuration.GetConnectionString("SmsDBConnection")));
            return services;
        }

    }
}
