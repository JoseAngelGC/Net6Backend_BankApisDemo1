using Ardalis.Specification;
using BancoApis.DomainModel.Entities;
using BancoApis.DomainServices.Dtos.Clients.Queries;

namespace BancoApis.ApplicationServices.Mediator.Features.Clients.Specifications
{
    internal class PageClientsSpecification : Specification<Client>
    {
        public PageClientsSpecification(GetAllClientsQuery requestParameters)
        {
            Query.Skip((int)(( requestParameters.PageNumber! - 1 ) * requestParameters.PageSize!))
                .Take((int)requestParameters.PageSize!);

            if (string.IsNullOrEmpty(requestParameters.Nombre))
                Query.Search(x => x.Name, "%" + requestParameters.Nombre + "%");
        }
    }
}
