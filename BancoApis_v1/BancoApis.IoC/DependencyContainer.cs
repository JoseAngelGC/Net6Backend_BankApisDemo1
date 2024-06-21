using Microsoft.Extensions.DependencyInjection;
using BancoApis.Persistence;
using Microsoft.Extensions.Configuration;
using BancoApis.ApplicationServices;
using BancoApis.OperationControllers;
using BancoApis.Auth;
namespace BancoApis.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependenciesIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthIdentityInfraestructure(configuration);
            services.AddAuthJwtInfraestructure(configuration);
            services.AddPercistenceInfraestructure(configuration);
            services.AddApiVersioningExtension();
            services.AddApplicationServices();
            
            return services;
        }

        public static void AddDependenciesIoCAsync(this IServiceCollection services)
        {
            services.AddAuthDefaultElementsInfraestructureAsync();
        }
    }
}
