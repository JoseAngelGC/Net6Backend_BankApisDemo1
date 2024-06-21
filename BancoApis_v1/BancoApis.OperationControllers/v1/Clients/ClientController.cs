using BancoApis.DomainServices.Dtos.Clients.Commands;
using BancoApis.DomainServices.Dtos.Clients.Queries;
using BancoApis.OperationControllers.v1.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancoApis.OperationControllers.v1.Clients
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpPut("Items")]
        public async Task<IActionResult> Get([FromBody] GetAllClientsQuery request)
        {

            return Ok(await Mediator.Send(request));
        }

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> Get(int id)
        {

            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }


        [HttpPost("New")]
        [Authorize]
        public async Task<IActionResult> AddItem(CreateClientCommand command)
        {
            return Created("", await Mediator.Send(command));
        }

        [HttpPut("NewData/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem(int id, UpdateClientCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("Detached/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id, [FromBody] DeleteClientCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}
