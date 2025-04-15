using Microsoft.AspNetCore.Mvc;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.API.Models.Errors;

namespace GestionDocente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        public readonly IApplicationUserService _userService;
        private readonly IApplicationRoleService _roleService;

        public ApplicationUsersController(IApplicationUserService userApplication, IApplicationRoleService roleService)
        {
            _userService = userApplication;
            _roleService = roleService;
        }

        #region Users

        [HttpGet]
        [ProducesResponseType<IEnumerable<UserResponseDto>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> Index()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("GetUserById/{id}", Name = "GetUserById")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        [HttpGet("GetUserByEmail/{email}", Name = "GetUserByEmail")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

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
        [Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser(CreateApplicationUserRequestDto user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (!string.IsNullOrEmpty(result))
            {
                return CreatedAtRoute("GetUserById", new { id = result }, user);
            }
            else
            {
                return BadRequest("Error al crear el usuario.");
            }
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUser(string userId, UpdateApplicationUserRequestDto user)
        {
            var userDB = await _userService.GetUserByIdAsync(userId);

            if (userDB == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var result = await _userService.UpdateUserAsync(userId, user);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al modificar el usuario.");
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var result = await _userService.DeleteUserAsync(userId);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Error al eliminar el usuario.");
            }
        }

        #endregion

        #region Roles

        [HttpGet]
        [Route("GetRolesAsync/{userId}")]

        [ProducesResponseType<IList<String>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<ActionResult<IList<String>>> GetRolesAsync(string userId)
        {
            var userRoles = await _userService.GetRolesAsync(userId);

            if (userRoles?.Count > 0)
            {
                return Ok(userRoles);
            }
            else
            {
                return NotFound("El usuario no está asignado a ningún rol.");
            }

        }

        [HttpPost]
        [Route("AddToRoleAsync/{userId}/{roleName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddToRoleAsync(string userId, string roleName)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("El nombre del rol no puede ser nulo o vacío.");
            }

            var roleExists = await _roleService.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                return NotFound($"No existe el rol {roleName}");
            }

            //verifico si el usuario ya tiene el rol asignado
            var userRoles = await _userService.GetRolesAsync(userId);

            if (userRoles.Any(role => role.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("El usuario ya tiene asignado el rol.");
            }

            var result = await _userService.AddToRoleAsync(userId, roleName);

            if (result)
            {
                return Ok("Rol asignado correctamente.");
            }
            else
            {
                return BadRequest("Error al asignar el rol.");
            }
        }

        [HttpPost]
        [Route("AddToRolesAsync/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddToRolesAsync(string userId,[FromBody] IEnumerable<string> roleNames)
        {

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if ((roleNames == null) || (!roleNames.Any()))
            {
                return BadRequest("Debe proporcionar una lista con los nombres de los roles.");
            }

            var result = await _userService.AddToRolesAsync(userId, roleNames);

            if (result)
            {
                return Ok("Roles asignados correctamente.");
            }
            else
            {
                return BadRequest("Error al asignar los roles.");
            }
        }

        [HttpPost]
        [Route("RemoveFromRoleAsync/{userId}/{roleName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("El nombre del rol no puede ser nulo o vacío.");
            }

            //verifico si el usuario ya tiene el rol asignado
            var userRoles = await _userService.GetRolesAsync(userId);

            if (!userRoles.Any(role => role.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("El usuario no tiene asignado el rol.");
            }

            var result = await _userService.RemoveFromRoleAsync(userId, roleName);
            if (result)
            {
                return Ok("Rol removido correctamente.");
            }
            else
            {
                return BadRequest("Error al remover el rol.");
            }
        }

        [HttpPost]
        [Route("RemoveFromRolesAsync/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveFromRolesAsync(string userId,[FromBody] IEnumerable<string> roleNames)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if ((roleNames == null) || (!roleNames.Any()))
            {
                return BadRequest("Debe proporcionar una lista con los nombres de los roles.");
            }

            var result = await _userService.RemoveFromRolesAsync(userId, roleNames);
            if (result)
            {
                return Ok("Roles removidos correctamente.");
            }
            else
            {
                return BadRequest("Error al remover los roles.");
            }
        }

        #endregion
    }
}
