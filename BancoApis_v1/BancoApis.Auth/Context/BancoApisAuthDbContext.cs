using BancoApis.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BancoApis.Auth.Context
{
    internal class BancoApisAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public BancoApisAuthDbContext(DbContextOptions<BancoApisAuthDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
