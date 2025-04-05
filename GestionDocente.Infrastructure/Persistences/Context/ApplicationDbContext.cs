using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionDocente.Infrastructure.Models;

namespace GestionDocente.Infrastructure.Persistences.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel, ApplicationRoleModel, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }
        public DbSet<ApplicationRoleModel> ApplicationRoles { get; set; }
    }
}
