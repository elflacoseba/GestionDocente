using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;
using System.Linq.Expressions;

namespace GestionDocente.Application.Interfaces
{
    public interface IInstitucionService
    {
        Task<IEnumerable<InstitucionResponseDto>> GetInstitucionesAsync();
        Task<InstitucionResponseDto?> GetInstitucionByIdAsync(string id);
        Task<IEnumerable<InstitucionResponseDto>> BuscarInstitucionesAsync(Expression<Func<Institucion, bool>> predicate);
        Task<InstitucionResponseDto> CreateInstitucionAsync(CreateInstitucionDto createInstitucionDto);
        Task<InstitucionResponseDto> UpdateInstitucionAsync(UpdateInstitucionDto updateInstitucionDto);
        Task<bool> DeleteInstitucionAsync(string id);
    }
}
