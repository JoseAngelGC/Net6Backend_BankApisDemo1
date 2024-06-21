using AutoMapper;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.DomainServices.Dtos.Clients.Commands;
using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Commands
{
    internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ResultResponse<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<ResultResponse<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var clientRecord = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            var updateRecord = _mapper.Map<Client>(request);
            updateRecord.CreatedBy = clientRecord.CreatedBy;
            updateRecord.CreatedDate = clientRecord.CreatedDate;
            await _repositoryAsync.UpdateAsync(updateRecord, cancellationToken);
            return new ResultResponse<int>("El registro se actualizó de manera exitosa", clientRecord.Id);
        }
    }
}
