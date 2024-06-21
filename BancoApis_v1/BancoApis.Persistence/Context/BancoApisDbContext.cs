using BancoApis.DomainModel.Commons;
using BancoApis.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BancoApis.Persistence.Context
{
    internal class BancoApisDbContext : DbContext
    {
        public BancoApisDbContext(DbContextOptions<BancoApisDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Client> Clients { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        DateTime created = DateTime.UtcNow;
                        DateTime modified = DateTime.UtcNow;
                        entry.Entity.CreatedDate = created;
                        entry.Entity.CreatedBy = "CreateUserAlias";
                        entry.Entity.LastModifiedBy = "CreateUserAlias";
                        entry.Entity.LastModifiedDate = modified;
                        entry.Entity.LastModifiedBy = "CreateUserAlias";
                        break;
                    case EntityState.Modified:
                        DateTime modifiedd = DateTime.UtcNow;
                        entry.Entity.LastModifiedDate = modifiedd;
                        entry.Entity.LastModifiedBy = "UpdateUserAlias";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
