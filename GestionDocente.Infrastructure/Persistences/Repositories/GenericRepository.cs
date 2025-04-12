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

        //public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        //}


        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    var result = await _dbSet.FindAsync(id);
        //    return result!;
        //}
        //public async Task AddAsync(TEntity entity)
        //{
        //   await _dbSet.AddAsync(entity);
        //}

        //public void Update(TEntity entity)
        //{
        //    _dbSet.Update(entity);            
        //}

        //public void Delete(int id)
        //{
        //    TEntity entity = _dbSet.Find(id)!;
        //    if (entity != null)
        //    {
        //        _dbSet.Remove(entity);
        //    }
        //}
    }
}
