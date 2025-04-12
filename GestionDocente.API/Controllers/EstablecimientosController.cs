using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Interfaces;
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
            var idGuid = Guid.TryParse(establecimientoId, out var idParsed);

            if (!idGuid)
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(new Guid(establecimientoId));

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            return Ok(establecimiento);
        }
    }
}
