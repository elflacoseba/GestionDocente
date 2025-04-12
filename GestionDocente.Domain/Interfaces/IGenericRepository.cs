using System.Linq.Expressions;

namespace GestionDocente.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(string id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(string id);
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TModel, bool>> predicate);
    }
}
