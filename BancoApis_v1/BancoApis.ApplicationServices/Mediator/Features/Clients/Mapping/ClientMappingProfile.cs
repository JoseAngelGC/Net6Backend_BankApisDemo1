using AutoMapper;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Dtos.Clients;
using BancoApis.DomainServices.Dtos.Clients.Commands;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Mapping
{
    internal class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            #region Commands
            CreateMap<CreateClientCommand, Client>();
            CreateMap<UpdateClientCommand, Client>();
            #endregion

            #region Dtos
            CreateMap<Client, ClientDto>();
            #endregion
        }
    }
}
