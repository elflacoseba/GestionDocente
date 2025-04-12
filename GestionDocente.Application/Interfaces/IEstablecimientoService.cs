using GestionDocente.Application.Dtos.Response;

namespace GestionDocente.Application.Interfaces
{
    public interface IEstablecimientoService
    {
        Task<IEnumerable<EstablecimientoDto>> GetEstablecimientosAsync();
        //Task<EstablecimientoDto> GetByIdAsync(int id);
        //Task CreateAsync(EstablecimientoDto establecimientoDto);
        //Task UpdateAsync(EstablecimientoDto establecimientoDto);
        //Task DeleteAsync(int id);
    }
}
