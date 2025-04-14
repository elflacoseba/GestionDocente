using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Models;
using System.Linq.Expressions;

namespace GestionDocente.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(string id);
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TModel, bool>> predicate);
    }
}
