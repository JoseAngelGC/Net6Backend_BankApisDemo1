using Microsoft.AspNetCore.Http;

namespace BancoApis.OperationControllers.v1.Helpers.Abstractions
{
    public interface IIpAddressResponseHelper
    {
        string GenerateIpAddress(HttpRequest request, HttpContext context);
    }
}
