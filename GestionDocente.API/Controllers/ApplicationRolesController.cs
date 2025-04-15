using Microsoft.AspNetCore.Mvc;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;

namespace GestionDocente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationRolesController : ControllerBase
    {
        public readonly IApplicationRoleService _roleService;

        public ApplicationRolesController(IApplicationRoleService roleApplication)
        {
            _roleService = roleApplication;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationRoleResponseDto>>> Index()
        {
            var roles = await _roleService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("{roleId}", Name = "GetRoleById")]
        public async Task<ActionResult<ApplicationRoleResponseDto>> GetRoleById(string roleId)
        {
            if (!Guid.TryParse(roleId, out var idGuidParsed))
            {
                return BadRequest("El id del rol no es válido.");
            }

            var roleDb = await _roleService.GetRoleByIdAsync(roleId);

            if (roleDb is null)
            {
                return NotFound("Rol no encontrado.");
            }

            return Ok(roleDb);
        }

        [HttpGet("GetRoleByName/{roleName}")]

        public async Task<ActionResult<ApplicationRoleResponseDto>> GetRoleByName(string roleName)
        {
            var user = await _roleService.GetRoleByNameAsync(roleName);

            if (user is null)
            {
                return NotFound("Rol no encontrado.");
            }

            return Ok(user);
        }

        [HttpPost("RoleExists/{roleName}")]
        public async Task<ActionResult> RoleExists(string roleName)
        {
            var result = await _roleService.RoleExistsAsync(roleName);

            if (result)
            {
                return Ok("El rol ya existe.");
            }
            else
            {
                return NotFound("El rol no existe.");
            }
        }

        [HttpPost("CreateRole")]        
        public async Task<IActionResult> CreateRole(CreateApplicationRoleRequestDto role)
        {
            var roleCreated = await _roleService.CreateRoleAsync(role);

            if (roleCreated is null)
            {
                return BadRequest("Error al crear el rol.");
            }

            return CreatedAtRoute("GetRoleById", new { roleId = roleCreated.Id }, roleCreated);
        }

        [HttpPut("UpdateRole/{roleId}")]
        public async Task<ActionResult> UpdateRole(string roleId, CreateApplicationRoleRequestDto role)
        {
            if (!Guid.TryParse(roleId, out var idGuidParsed))
            {
                return BadRequest("El id del rol no es válido.");
            }

            var roleDb = await _roleService.GetRoleByIdAsync(roleId);

            if (roleDb == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var updateRoleDto = new UpdateApplicationRoleRequestDto
            {
                Name = role.Name,
                Description = role.Description
            };

            updateRoleDto.SetId(roleId);


            var result = await _roleService.UpdateRoleAsync(updateRoleDto);

            if (!result)
            {
                return BadRequest("Error al modificar el rol.");
            }

            return NoContent();
        }

        [HttpDelete("DeleteRole)/{roleId}")]
        public async Task<ActionResult> DeleteRole(string roleId)
        {
            if (!Guid.TryParse(roleId, out var idGuidParsed))
            {
                return BadRequest("El id del rol no es válido.");
            }

            var roleDb = await _roleService.GetRoleByIdAsync(roleId);

            if (roleDb == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var result = await _roleService.DeleteRoleAsync(roleId);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al eliminar el rol.");
            }
        }
    }
}
