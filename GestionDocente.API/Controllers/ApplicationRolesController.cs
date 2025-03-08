using Microsoft.AspNetCore.Mvc;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.API.Models.Errors;

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
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetRolesAsync();

            return Ok(roles);
        }
        
        [HttpGet]
        [Route("GetRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetRoleById(string roleId)
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

        [HttpGet]
        [Route("GetRoleByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> GetRoleByName(string roleName)
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

        [HttpPost]
        [Route("RoleExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RoleExists(string roleName)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole(CreateApplicationRoleRequestDto role)
        {
            var result = await _roleService.CreateRoleAsync(role);
            if (result)
            {
                return Ok("Rol creado correctamente.");
            }
            else
            {
                return BadRequest("Error al crear el rol.");
            }
        }

        [HttpPatch]
        [Route("UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole(UpdateApplicationRoleRequestDto role)
        {
            var userDB = await _roleService.GetRoleByIdAsync(role.Id!);

            if (userDB == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var result = await _roleService.UpdateRoleAsync(role);

            if (result)
            {
                return Ok("Rol modificado correctamente.");
            }
            else
            {
                return BadRequest("Error al modificar el rol.");
            }
        }

        [HttpPost]
        [Route("DeleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Rol no encontrado.");
            }

            var result = await _roleService.DeleteRoleAsync(roleId);

            if (result)
            {
                return Ok("Rol eliminado correctamente.");
            }
            else
            {
                return BadRequest("Error al eliminar el rol.");
            }
        }
    }
}
