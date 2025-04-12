using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Domain.Models;
using GestionDocente.Infrastructure.Persistences.Context;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class EstablecimientoRepository : GenericRepository<Establecimiento, EstablecimientoModel>, IEstablecimientoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EstablecimientoRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
