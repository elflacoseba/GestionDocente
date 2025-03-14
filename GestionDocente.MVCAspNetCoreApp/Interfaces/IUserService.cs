using GestionDocente.MVCAspNetCoreApp.Models;

namespace GestionDocente.MVCAspNetCoreApp.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task<ApplicationUser> UpdateUserAsync(string id, ApplicationUser user);
        Task DeleteUserAsync(string id);
    }
}
