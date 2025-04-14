using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Domain.Models;
using GestionDocente.Infrastructure.Persistences.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
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
            var model = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return null;
            }

            return _mapper.Map<TEntity>(model);
        }

        public async Task AddAsync(TEntity entity)
        {
            var modelDb = _mapper.Map<TModel>(entity);

            await _dbSet.AddAsync(modelDb);
        }

        public async Task Update(TEntity entity)
        {          
            var modelDb = await _dbSet.FindAsync(entity.Id.ToString());

            _context.Entry(modelDb!).State = EntityState.Detached; // Detach the entity to avoid tracking issues

            modelDb = _mapper.Map<TModel>(entity);

            _context.Entry(modelDb).Property(x => x.UsuarioCreacion).IsModified = false;
            _context.Entry(modelDb).Property(x => x.FechaCreacion).IsModified = false;

            _dbSet.Update(modelDb!);
        }

        public async Task Delete(string id)
        {
            var modelDb = await _dbSet.FindAsync(id);
            
            if (modelDb != null)
            {
                _dbSet.Remove(modelDb);
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
