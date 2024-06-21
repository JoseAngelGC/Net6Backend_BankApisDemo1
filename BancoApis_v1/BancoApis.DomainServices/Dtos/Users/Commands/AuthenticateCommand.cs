using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.DomainServices.Dtos.Users.Commands
{
    public class AuthenticateCommand : IRequest<ResultResponse<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
