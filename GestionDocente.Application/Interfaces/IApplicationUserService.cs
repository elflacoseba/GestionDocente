using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;

namespace GestionDocente.Application.Interfaces
{
    public interface IApplicationUserService
    {
        #region Users
        
        Task<IEnumerable<UserResponseDto>> GetUsersAsync();
        Task<UserResponseDto?> GetUserByUsernameAsync(string username);
        Task<UserResponseDto?> GetUserByIdAsync(string userId);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task<string> CreateUserAsync(CreateApplicationUserRequestDto user);
        Task<bool> UpdateUserAsync(string userId, UpdateApplicationUserRequestDto user);
        Task<bool> DeleteUserAsync(string userId);
        
        #endregion

        #region Roles

        Task<IList<string>> GetRolesAsync(string userId);
        Task<bool> AddToRoleAsync(string userId, string roleName);
        Task<bool> AddToRolesAsync(string userId, IEnumerable<string> roleNames);
        Task<bool> RemoveFromRoleAsync(string userId, string roleName);
        Task<bool> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames);

        #endregion
    }
}
