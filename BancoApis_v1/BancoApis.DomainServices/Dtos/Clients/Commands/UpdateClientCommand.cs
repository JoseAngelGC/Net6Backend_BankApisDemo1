using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.DomainServices.Dtos.Clients.Commands
{
    public class UpdateClientCommand : IRequest<ResultResponse<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
