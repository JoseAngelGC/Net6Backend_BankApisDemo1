using BancoApis.DomainModel.Auth;
using Microsoft.AspNetCore.Identity;

namespace BancoApis.Auth.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string Name { get ; set ; }
    }
}
