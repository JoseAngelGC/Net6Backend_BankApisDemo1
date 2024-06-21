using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.DomainServices.Dtos.Clients.Commands
{
    public class DeleteClientCommand : IRequest<ResultResponse<int>>
    {
        public int Id { get; set; }
    }
}
