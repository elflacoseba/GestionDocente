using Microsoft.AspNetCore.Identity;

namespace GestionDocente.Domain.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
