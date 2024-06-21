using BancoApis.DomainServices.Abstractions.Commons;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.Persistence.Context;
using BancoApis.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoApis.Persistence
{
    public static class DependencyContainer
    {
        public static void AddPercistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext service register
            var assembly = typeof(BancoApisDbContext).Assembly.FullName;
            services.AddDbContext<BancoApisDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("SqlConnectionDev"),
                opt => opt.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                );

            services.AddScoped(typeof(IRepositoryAsync<>), typeof(GenericRepository<>));
        }
    }
}
