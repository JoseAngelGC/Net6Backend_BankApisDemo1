using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.DomainServices.Dtos.Clients.Queries
{
    public class GetClientByIdQuery : IRequest<ResultResponse<ClientDto>>
    {
        public int Id { get; set; }
    }
}
