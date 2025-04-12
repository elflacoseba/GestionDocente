using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GestionDocente.Infrastructure.Persistences.Context;
using GestionDocente.Domain.Interfaces;
using AutoMapper;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TModel> _dbSet;
        private readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TModel>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var models = await _dbSet.AsNoTracking().ToListAsync();
            
            return  _mapper.Map<IEnumerable<TEntity>>(models);
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            var model = await _dbSet.FindAsync(id);
            
            if (model == null)
            {
                return null;
            }

            return _mapper.Map<TEntity>(model);
        }

        public async Task AddAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);

            await _dbSet.AddAsync(model);
        }

        public void Update(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);

             _dbSet.Update(model);
        }

        public void Delete(string id)
        {
            TModel model = _dbSet.Find(id)!;
            
            if (model != null)
            {
                _dbSet.Remove(model);
            }
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TModel, bool>> predicate)
        {
            var models = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

            // Map the results back to TEntity
            return _mapper.Map<IEnumerable<TEntity>>(models);
        }        
    }
}
