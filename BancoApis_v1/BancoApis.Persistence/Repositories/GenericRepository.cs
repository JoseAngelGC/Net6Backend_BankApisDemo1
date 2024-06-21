using Ardalis.Specification.EntityFrameworkCore;
using BancoApis.DomainServices.Abstractions.Repositories;
using BancoApis.Persistence.Context;

namespace BancoApis.Persistence.Repositories
{
    internal class GenericRepository<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly BancoApisDbContext _context;
        public GenericRepository(BancoApisDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
