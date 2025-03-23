using Microsoft.AspNetCore.Mvc;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.API.Models.Errors;
using GestionDocente.Application.Dtos.Response;

namespace GestionDocente.SecureIAM_API.Controllers
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
        [ProducesResponseType<IEnumerable<RoleResponseDto>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> Index()
        {
            var roles = await _roleService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("GetRoleById/{roleId}", Name = "GetRoleById")]
        [ProducesResponseType<RoleResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleResponseDto>> GetRoleById(string roleId)
        {
            var user = await _roleService.GetRoleByIdAsync(roleId);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Rol no encontrado.");
            }
        }

        [HttpGet("GetRoleByName/{roleName}", Name = "GetRoleByName")]
        [ProducesResponseType<RoleResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleResponseDto>> GetRoleByName(string roleName)
        {
            var user = await _roleService.GetRoleByNameAsync(roleName);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        [HttpPost("RoleExists/{roleName}", Name = "RoleExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [Route("CreateRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateRole(CreateApplicationRoleRequestDto role)
        {
            var result = await _roleService.CreateRoleAsync(role);

            if (!string.IsNullOrEmpty(result))
            {
                return CreatedAtRoute("GetRoleById", new { roleId = result }, role);
            }
            else
            {
                return BadRequest("Error al crear el rol.");
            }
        }

        [HttpPut("{roleId}")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRole(string roleId, UpdateApplicationRoleRequestDto role)
        {
            var userDB = await _roleService.GetRoleByIdAsync(roleId);

            if (userDB == null)
            {
                return NotFound("Rol no encontrado.");
            }            

            var result = await _roleService.UpdateRoleAsync(roleId, role);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al modificar el rol.");
            }
        }

        [HttpDelete("{roleId}")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteRole(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);

            if (role == null)
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
