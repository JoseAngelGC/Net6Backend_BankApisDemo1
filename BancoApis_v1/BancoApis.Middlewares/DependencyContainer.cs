using BancoApis.Middlewares.Errors;
using Microsoft.AspNetCore.Builder;

namespace BancoApis.Middlewares
{
    public static class DependencyContainer
    {
        public static void UseErrorHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
