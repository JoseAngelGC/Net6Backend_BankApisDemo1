using BancoApis.DomainServices.Dtos.Requests;
using BancoApis.DomainServices.Dtos.Users.Commands;
using BancoApis.OperationControllers.v1.Bases;
using BancoApis.OperationControllers.v1.Helpers.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BancoApis.OperationControllers.v1.Users
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly IIpAddressResponseHelper _ipAddressHelper;
        public AccountController(IIpAddressResponseHelper ipAddressResponseHelper)
        {
            _ipAddressHelper = ipAddressResponseHelper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AutheticationAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = _ipAddressHelper.GenerateIpAddress(Request, HttpContext)
            }));
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Name = request.Name,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["Origin"]
            }));
        }

    }
}
