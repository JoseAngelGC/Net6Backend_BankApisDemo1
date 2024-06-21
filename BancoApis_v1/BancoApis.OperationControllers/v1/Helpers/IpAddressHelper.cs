using BancoApis.OperationControllers.v1.Helpers.Abstractions;
using Microsoft.AspNetCore.Http;

namespace BancoApis.OperationControllers.v1.Helpers
{
    internal class IpAddressHelper : IIpAddressResponseHelper
    {

        public string GenerateIpAddress(HttpRequest request, HttpContext context)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return request.Headers["X-Forwarded-For"];
            }
            else
            {
                return context.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            }
        }
    }
}
