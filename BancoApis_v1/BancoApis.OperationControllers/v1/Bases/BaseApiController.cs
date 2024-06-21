using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace BancoApis.OperationControllers.v1.Bases
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Description("[CurrencyCatalog EndPoints]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
