using GestionDocente.MVCAspNetCoreApp.Interfaces;
using GestionDocente.MVCAspNetCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestionDocente.MVCAspNetCoreApp.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<ApplicationUsersController> _logger;

        public ApplicationUsersController(
            IUserService userService,
            ILogger<ApplicationUsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationUsers = await _userService.GetAllUsersAsync();
                return View(applicationUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el listado de usuarios.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
