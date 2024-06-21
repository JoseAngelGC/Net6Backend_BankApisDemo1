using AutoMapper;
using BancoApis.ApplicationServices.Mediator.Features.Clients.Specifications;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.DomainServices.Dtos.Clients;
using BancoApis.DomainServices.Dtos.Clients.Queries;
using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Queries
{
    internal class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDto>>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetAllClientsQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<PagedResponse<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clientsList = await _repositoryAsync.ListAsync(new PageClientsSpecification(request), cancellationToken);
            var ClientsDto = _mapper.Map<List<ClientDto>>(clientsList);

            return new PagedResponse<List<ClientDto>>(ClientsDto, (int)request.PageNumber!, (int)request.PageSize!, "Consulta se realizó exitosamente.");
        }
    }
}
