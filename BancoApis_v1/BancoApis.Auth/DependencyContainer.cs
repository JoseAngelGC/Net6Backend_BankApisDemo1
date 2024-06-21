using BancoApis.Auth.Context;
using BancoApis.Auth.Models;
using BancoApis.Auth.Seeds;
using BancoApis.Auth.Services;
using BancoApis.DomainModel.Auth;
using BancoApis.DomainServices.Abstractions.Services;
using BancoApis.DomainServices.Dtos.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace BancoApis.Auth
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddAuthIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Register AddContext service
            var assembly = typeof(BancoApisAuthDbContext).Assembly.FullName;
            services.AddDbContext<BancoApisAuthDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionDev"),
                opt => opt.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                );
            
            //Register Identity service
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BancoApisAuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAccountService, AccountService>();

            return services;

        }


        public static async void AddAuthDefaultElementsInfraestructureAsync(this IServiceCollection services)
        {
            //Run identity on execution time to create default users and roles
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                try
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultAdminUser.SeedAsync(userManager, roleManager);
                    await DefaultBasicUser.SeedAsync(userManager, roleManager);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }


        public static void AddAuthJwtInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync(context.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new ResultResponse<string>("Usted no está autorizado."));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "Application/Json";
                        var result = JsonSerializer.Serialize(new ResultResponse<string>("Usted no tiene permisos para este recurso."));
                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
    }
}
