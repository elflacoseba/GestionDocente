namespace GestionDocente.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        void BeginTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
