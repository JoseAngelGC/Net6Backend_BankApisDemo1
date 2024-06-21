using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.DomainServices.Dtos.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<PagedResponse<List<ClientDto>>>
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string? Nombre { get; set; } = null;
    }
}
