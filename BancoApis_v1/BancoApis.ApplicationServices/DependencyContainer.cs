using BancoApis.ApplicationServices.Mediator.Behaviours;
using BancoApis.ApplicationServices.Mediator.Features.Users.Validators;
using BancoApis.DomainServices.Validators.Clients;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BancoApis.ApplicationServices
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services register
            services.AddValidatorsFromAssemblyContaining<CreateClientCommandValidator>(ServiceLifetime.Transient);
            services.AddValidatorsFromAssemblyContaining<UpdateClientCommandValidator>(ServiceLifetime.Transient);
            services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>(ServiceLifetime.Transient);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
