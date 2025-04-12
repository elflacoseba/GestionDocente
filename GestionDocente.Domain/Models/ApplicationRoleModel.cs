using Microsoft.AspNetCore.Identity;

namespace GestionDocente.Domain.Models
{
    public class ApplicationRoleModel : IdentityRole
    {
        public string? Description { get; set; }
    }
}
