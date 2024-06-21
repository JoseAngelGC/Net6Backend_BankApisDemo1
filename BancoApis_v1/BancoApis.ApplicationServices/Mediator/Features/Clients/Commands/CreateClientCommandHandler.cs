using AutoMapper;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.DomainServices.Dtos.Clients.Commands;
using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Commands
{
    internal class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ResultResponse<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<ResultResponse<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Client>(request);
            var data = await _repositoryAsync.AddAsync(newRecord, cancellationToken);
            return new ResultResponse<int>("Operación exitosa", data.Id);
        }
    }
}
