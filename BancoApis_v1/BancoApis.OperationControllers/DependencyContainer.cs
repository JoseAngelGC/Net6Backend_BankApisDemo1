using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using BancoApis.OperationControllers.v1.Helpers;
using BancoApis.OperationControllers.v1.Helpers.Abstractions;

namespace BancoApis.OperationControllers
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddScoped<IIpAddressResponseHelper, IpAddressHelper>();

            return services;
        }
    }
}
