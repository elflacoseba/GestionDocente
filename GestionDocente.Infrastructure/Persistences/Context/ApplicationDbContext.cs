using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionDocente.Infrastructure.Models;
using System.Reflection;

namespace GestionDocente.Infrastructure.Persistences.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel, ApplicationRoleModel, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }
        public DbSet<ApplicationRoleModel> ApplicationRoles { get; set; }
        public DbSet<InstitucionModel> Instituciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
