using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Infrastructure.Models;
using GestionDocente.Infrastructure.Persistences.Context;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class InstitucionesRepository : GenericRepository<Institucion, InstitucionModel>, IInstitucionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InstitucionesRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
