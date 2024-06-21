using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.DomainServices.Dtos.Clients.Commands;
using BancoApis.DomainServices.Dtos.Responses;
using MediatR;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Commands
{
    internal class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ResultResponse<int>>
    {
        private readonly IRepositoryAsync<Client> _repositoryAsync;
        public DeleteClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<ResultResponse<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id, cancellationToken) ?? 
                throw new KeyNotFoundException($"No se encontró registro con el id {request.Id}");
            await _repositoryAsync.DeleteAsync(client, cancellationToken);
            return new ResultResponse<int>("El registro fue eliminado de manera exitosa.",client.Id);
        }
    }
}
