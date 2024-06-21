using Ardalis.Specification;

namespace BancoApis.DomainServices.Abstractions.Repositories
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
    }
}
