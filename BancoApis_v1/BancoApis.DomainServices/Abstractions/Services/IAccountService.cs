using BancoApis.DomainServices.Dtos.Requests;
using BancoApis.DomainServices.Dtos.Responses;

namespace BancoApis.DomainServices.Abstractions.Services
{
    public interface IAccountService
    {
        Task<ResultResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<ResultResponse<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}
