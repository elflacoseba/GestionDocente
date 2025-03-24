using Microsoft.AspNetCore.Mvc;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.API.Models.Errors;

namespace GestionDocente.SecureIAM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        public readonly IApplicationUserService _userService;

        public ApplicationUsersController(IApplicationUserService userApplication)
        {
            _userService = userApplication;
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
        [Route("GetRolesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> GetRolesAsync(string userId)
        {
            var userRoles = await _userService.GetRolesAsync(userId);

            if (userRoles != null)
            {
                return Ok(userRoles);
            }
            else
            {
                return NotFound("El usuario no está asignado a ningún rol.");
            }
        }

        [HttpPost]
        [Route("AddToRoleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToRoleAsync(string userId, string roleName)
        {
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
        [Route("AddToRolesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
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
        [Route("RemoveFromRoleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveFromRoleAsync(string userId, string roleName)
        {
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
        [Route("RemoveFromRolesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
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
