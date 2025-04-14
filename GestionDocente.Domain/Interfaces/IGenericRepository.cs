using GestionDocente.Domain.Entities;
using System.Linq.Expressions;

namespace GestionDocente.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity        
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(string id);
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
