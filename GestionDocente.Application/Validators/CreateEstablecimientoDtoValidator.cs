using FluentValidation;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Interfaces;

namespace GestionDocente.Application.Validators
{
    public class CreateEstablecimientoDtoValidator : AbstractValidator<CreateEstablecimientoDto>
    {
        private readonly IEstablecimientoService _establecimientoService;

        public CreateEstablecimientoDtoValidator(IEstablecimientoService establecimientoService)
        {
            _establecimientoService = establecimientoService;

            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre del establecimiento no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre del establecimiento no puede ser vacío.");

            When(Establecimiento => !string.IsNullOrEmpty(Establecimiento.Nombre), () =>
            {
                RuleFor(x => x.Nombre)
                .MustAsync(NombreEstablecimientoLibre).WithMessage("Ya existe un establecimiento con el mismo nombre");
            });

        }

        // <summary>
        /// Verifica si el nombre del establecimiento está libre. (No existe en la base de datos).
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> NombreEstablecimientoLibre(string? nombre, CancellationToken token)
        {
            if (string.IsNullOrEmpty(nombre))
                return false;

            var establecimientos = await _establecimientoService.BuscarEstablecimientosAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

            return establecimientos.Any() == false;

        }
    }
}
