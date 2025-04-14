using Microsoft.AspNetCore.Identity;

namespace GestionDocente.Infrastructure.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
