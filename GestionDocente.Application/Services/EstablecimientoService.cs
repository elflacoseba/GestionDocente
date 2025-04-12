using AutoMapper;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Interfaces;
using GestionDocente.Domain.Interfaces;
using System.Threading.Tasks;

namespace GestionDocente.Application.Services
{
    public class EstablecimientoService : IEstablecimientoService
    {
        private readonly IEstablecimientoRepository _establecimientoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstablecimientoService(IEstablecimientoRepository establecimientoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _establecimientoRepository = establecimientoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<EstablecimientoDto>> GetEstablecimientosAsync()
        {
            var establecimientosEntity =  await _establecimientoRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EstablecimientoDto>>(establecimientosEntity);
        }

        public async Task<EstablecimientoDto> GetEstablecimientosByIdAsync(Guid id)
        {
            var establecimiento =  await _establecimientoRepository.GetByIdAsync(id.ToString());

            return _mapper.Map<EstablecimientoDto>(establecimiento);
        }

        public async Task<bool> DeleteEstablecimientoAsync(Guid id)
        {
            _establecimientoRepository.Delete(id.ToString());

           return  await _unitOfWork.CommitAsync();
        }
    }
}
