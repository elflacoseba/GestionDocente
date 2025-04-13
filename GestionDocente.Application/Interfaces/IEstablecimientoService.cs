using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Models;
using System.Linq.Expressions;

namespace GestionDocente.Application.Interfaces
{
    public interface IEstablecimientoService
    {
        Task<IEnumerable<EstablecimientoResponseDto>> GetEstablecimientosAsync();
        Task<EstablecimientoResponseDto> GetEstablecimientosByIdAsync(Guid id);
        Task<IEnumerable<EstablecimientoResponseDto>> BuscarEstablecimientosAsync(Expression<Func<EstablecimientoModel, bool>> predicate);
        Task<EstablecimientoResponseDto> CreateEstablecimientoAsync(CreateEstablecimientoDto createEstablecimientoDto);
        Task<EstablecimientoResponseDto> UpdateEstablecimientoAsync(UpdateEstablecimientoDto updateEstablecimientoDto);
        Task<bool> DeleteEstablecimientoAsync(Guid id);
    }
}
