using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionDocente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionesController : ControllerBase
    {
        private readonly IInstitucionService _institucionService;

        public InstitucionesController(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> Index()
        {
            var instituciones = await _institucionService.GetInstitucionesAsync();

            return Ok(instituciones);
        }

        [HttpGet("{institucionId}", Name = "GetByIdAsync")]
        public async Task<ActionResult<InstitucionResponseDto>> GetInstitucionByIdAsync(string institucionId)
        {
            if (!Guid.TryParse(institucionId, out var idGuidParsed))
            {
                return BadRequest("El id de la institución no es válido.");
            }

            var institucion = await _institucionService.GetInstitucionByIdAsync(idGuidParsed.ToString());

            if (institucion is null)
            {
                return NotFound("Institución no encontrada.");
            }

            return Ok(institucion);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateInstitucionAsync(CreateInstitucionDto createInstitucionDto)
        {
            var institucionCreated = await _institucionService.CreateInstitucionAsync(createInstitucionDto);

            if (institucionCreated is null)
            {
                return BadRequest("Error al crear la institución.");
            }

            return CreatedAtRoute("GetByIdAsync", new { institucionId = institucionCreated.Id }, institucionCreated);
        }

        [HttpPut("Update/{institucionId}")]
        public async Task<ActionResult> UpdateInstitucionAsync(string institucionId, UpdateInstitucionDto updateInstitucionDto)
        {
            if (!Guid.TryParse(institucionId, out var idGuidParsed))
            {
                return BadRequest("El id de la institución no es válido.");
            }

            var institucion = await _institucionService.GetInstitucionByIdAsync(idGuidParsed.ToString());

            if (institucion is null)
            {
                return NotFound("Institución no encontrada.");
            }

            //Paso el id de la institución al DTO.
            updateInstitucionDto.SetId(institucionId);

            var result = await _institucionService.UpdateInstitucionAsync(updateInstitucionDto);

            if (result is null)
            {
                return BadRequest("Error al modificar la institución.");
            }

            return NoContent();
        }

        [HttpDelete("Delete/{institucionId}")]
        public async Task<ActionResult> DeleteInstitucionAsync(string institucionId)
        {
            if (!Guid.TryParse(institucionId, out var idGuidParsed))
            {
                return BadRequest("El id de la institución no es válido.");
            }

            var institucion = await _institucionService.GetInstitucionByIdAsync(idGuidParsed.ToString());

            if (institucion is null)
            {
                return NotFound("Institución no encontrada.");
            }

            var result = await _institucionService.DeleteInstitucionAsync(idGuidParsed.ToString());

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al eliminar la institución.");
            }
        }
    }       
}
