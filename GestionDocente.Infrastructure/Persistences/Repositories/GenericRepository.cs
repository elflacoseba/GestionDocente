using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Infrastructure.Models;
using GestionDocente.Infrastructure.Common;
using GestionDocente.Infrastructure.Persistences.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TModel> _dbSet;

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

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var visitor = new TypeConversionVisitor(typeof(TEntity), typeof(TModel));
            var convertedExpression = visitor.Convert<TEntity, TModel>(predicate);

            var models = await _dbSet.AsNoTracking().Where(convertedExpression).ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>>(models);
        }
        
    }
}
