namespace BancoApis.DomainServices.Abstractions.Requests
{
    public abstract class BasePagedRequest
    {
        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
    }
}
