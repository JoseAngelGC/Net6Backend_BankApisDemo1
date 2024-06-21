using BancoApis.DomainServices.Abstractions.Services;
using BancoApis.DomainServices.Dtos.Requests;
using BancoApis.DomainServices.Dtos.Responses;
using BancoApis.DomainServices.Dtos.Users.Commands;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Users.Commands
{
    internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, ResultResponse<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;
        public AuthenticateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<ResultResponse<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password,
            }, request.IpAddress);
        }

    }
}
