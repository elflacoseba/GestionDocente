using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;
using System.Linq.Expressions;

namespace GestionDocente.Application.Interfaces
{
    public interface IEstablecimientoService
    {
        Task<IEnumerable<EstablecimientoResponseDto>> GetEstablecimientosAsync();
        Task<EstablecimientoResponseDto?> GetEstablecimientosByIdAsync(string id);
        Task<IEnumerable<EstablecimientoResponseDto>> BuscarEstablecimientosAsync(Expression<Func<Establecimiento, bool>> predicate);
        Task<EstablecimientoResponseDto> CreateEstablecimientoAsync(CreateEstablecimientoDto createEstablecimientoDto);
        Task<EstablecimientoResponseDto> UpdateEstablecimientoAsync(UpdateEstablecimientoDto updateEstablecimientoDto);
        Task<bool> DeleteEstablecimientoAsync(string id);
    }
}
