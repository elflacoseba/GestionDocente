using System.Linq.Expressions;

namespace GestionDocente.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        //Task<TEntity> GetByIdAsync(int id);
        //Task AddAsync(TEntity entity);
        //void Update(TEntity entity);
        //void Delete(int id);
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
