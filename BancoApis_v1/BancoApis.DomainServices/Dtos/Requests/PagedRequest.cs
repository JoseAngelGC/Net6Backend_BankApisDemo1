using BancoApis.DomainServices.Abstractions.Requests;

namespace BancoApis.DomainServices.Dtos.Requests
{
    internal class PagedRequest : BasePagedRequest
    {

        public PagedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize <= 10 ? 10 : pageSize;
        }
    }
}
