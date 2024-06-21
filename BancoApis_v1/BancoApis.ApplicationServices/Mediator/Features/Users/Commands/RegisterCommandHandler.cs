using BancoApis.DomainServices.Abstractions.Services;
using BancoApis.DomainServices.Dtos.Requests;
using BancoApis.DomainServices.Dtos.Responses;
using BancoApis.DomainServices.Dtos.Users.Commands;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Users.Commands
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultResponse<string>>
    {
        private readonly IAccountService _accountService;
        public RegisterCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<ResultResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return _accountService.RegisterAsync(new RegisterRequest 
            { 
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
            }, request.Origin);
        }
    }
}
