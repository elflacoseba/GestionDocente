using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Interfaces;
using GestionDocente.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDocente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablecimientosController : ControllerBase
    {
        private readonly IEstablecimientoService _establecimientoService;

        public EstablecimientosController(IEstablecimientoService establecimientoService)
        {
            _establecimientoService = establecimientoService;
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> Index()
        {
            var establecimientos = await _establecimientoService.GetEstablecimientosAsync();

            return Ok(establecimientos);
        }

        [HttpGet("GetEstablecimientoById/{establecimientoId}")]
        public async Task<ActionResult<EstablecimientoDto>> GetEstablecimientoByIdAsync(string establecimientoId)
        {            
            if (!Guid.TryParse(establecimientoId, out var idGuidParsed))
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(idGuidParsed);

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            return Ok(establecimiento);
        }

        [HttpPost("DeleteEstablecimiento/{establecimientoId}")]
        public async Task<ActionResult> DeleteRole(string establecimientoId)
        {
            if (!Guid.TryParse(establecimientoId, out var idGuidParsed))
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(idGuidParsed);

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            var result = await _establecimientoService.DeleteEstablecimientoAsync(idGuidParsed);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al eliminar el establecimiento.");
            }
        }
    }
}
