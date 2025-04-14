using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Interfaces;
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

        [HttpGet("{establecimientoId}", Name = "GetByIdAsync")]
        public async Task<ActionResult<EstablecimientoResponseDto>> GetEstablecimientoByIdAsync(string establecimientoId)
        {
            if (!Guid.TryParse(establecimientoId, out var idGuidParsed))
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(idGuidParsed.ToString());

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            return Ok(establecimiento);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateEstablecimientoAsync(CreateEstablecimientoDto createEstablecimientoDto)
        {
            var establecimientoCreated = await _establecimientoService.CreateEstablecimientoAsync(createEstablecimientoDto);

            if (establecimientoCreated is null)
            {
                return BadRequest("Error al crear el establecimiento.");
            }

            return CreatedAtRoute("GetByIdAsync", new { establecimientoId = establecimientoCreated.Id }, establecimientoCreated);
        }

        [HttpPut("Update/{establecimientoId}")]
        public async Task<ActionResult> UpdateEstablecimientoAsync(string establecimientoId, UpdateEstablecimientoDto updateEstablecimientoDto)
        {
            if (!Guid.TryParse(establecimientoId, out var idGuidParsed))
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(idGuidParsed.ToString());

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            //Paso el id del establecimiento al DTO.
            updateEstablecimientoDto.SetId(establecimientoId);

            var result = await _establecimientoService.UpdateEstablecimientoAsync(updateEstablecimientoDto);

            if (result is null)
            {
                return BadRequest("Error al modificar el rol.");
            }

            return NoContent();
        }

        [HttpDelete("Delete/{establecimientoId}")]
        public async Task<ActionResult> DeleteAsync(string establecimientoId)
        {
            if (!Guid.TryParse(establecimientoId, out var idGuidParsed))
            {
                return BadRequest("El id del establecimiento no es válido.");
            }

            var establecimiento = await _establecimientoService.GetEstablecimientosByIdAsync(idGuidParsed.ToString());

            if (establecimiento is null)
            {
                return NotFound("Establecimiento no encontrado.");
            }

            var result = await _establecimientoService.DeleteEstablecimientoAsync(idGuidParsed.ToString());

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
