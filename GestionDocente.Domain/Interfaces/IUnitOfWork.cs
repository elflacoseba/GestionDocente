namespace GestionDocente.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository ApplicationUsers { get; }
        IApplicationRoleRepository ApplicationRoles { get; }
        IEstablecimientoRepository Establecimientos { get; }

        void BeginTransaction();
        Task<bool> CommitAsync();
        Task RollbackAsync();
    }
}
