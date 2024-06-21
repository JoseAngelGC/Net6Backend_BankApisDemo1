using AutoMapper;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.DomainServices.Dtos.Clients;
using BancoApis.DomainServices.Dtos.Clients.Queries;
using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Queries
{
    internal class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ResultResponse<ClientDto>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetClientByIdQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<ResultResponse<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken) ??
                throw new KeyNotFoundException($"No se encontró registro con el id {request.Id}");

            var clientDto = _mapper.Map<ClientDto>(client);
            return new ResultResponse<ClientDto>("La consulta fue exitosa.", clientDto);
        }
    }
}
