namespace GestionDocente.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUsers { get; }
        IApplicationRoleRepository ApplicationRoles { get; }
        IInstitucionRepository Instituciones { get; }

        void BeginTransaction();
        Task<bool> CommitAsync();
        Task RollbackAsync();
    }
}
