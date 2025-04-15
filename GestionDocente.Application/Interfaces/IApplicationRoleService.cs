using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;

namespace GestionDocente.Application.Interfaces
{
    public interface IApplicationRoleService
    {
        Task<IEnumerable<ApplicationRoleResponseDto>> GetRolesAsync();
        Task<ApplicationRoleResponseDto?> GetRoleByIdAsync(string roleId);
        Task<ApplicationRoleResponseDto?> GetRoleByNameAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<ApplicationRoleResponseDto> CreateRoleAsync(CreateApplicationRoleRequestDto role);
        Task<bool> UpdateRoleAsync(UpdateApplicationRoleRequestDto role);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
