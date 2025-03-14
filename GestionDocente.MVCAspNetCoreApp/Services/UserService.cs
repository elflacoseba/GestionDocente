using GestionDocente.MVCAspNetCoreApp.Interfaces;
using GestionDocente.MVCAspNetCoreApp.Models;

namespace GestionDocente.MVCAspNetCoreApp.Services
{
    public class UserService : IUserService
    {
        private readonly IApiClient _apiClient;
        private const string BasePath = "api/applicationusers";

        public UserService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _apiClient.GetAsync<IEnumerable<ApplicationUser>>(BasePath);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _apiClient.GetAsync<ApplicationUser>($"{BasePath}/{id}");
        }

        public Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UpdateUserAsync(string id, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        // Implementar otros métodos...
    }
}
