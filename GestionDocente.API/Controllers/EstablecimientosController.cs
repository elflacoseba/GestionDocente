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
        [ProducesResponseType<IEnumerable<EstablecimientoDto>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> Index()
        {
            var establecimientos = await _establecimientoService.GetEstablecimientosAsync();

            return Ok(establecimientos);
        }
    }
}
