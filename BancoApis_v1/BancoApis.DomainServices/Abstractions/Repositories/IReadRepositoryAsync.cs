using Ardalis.Specification;

namespace BancoApis.DomainServices.Abstractions.Repositories
{
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class
    {
    }
}
