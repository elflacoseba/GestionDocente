using FluentValidation;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Interfaces;

namespace GestionDocente.Application.Validators
{
    public class CreateInstitucionDtoValidator : AbstractValidator<CreateInstitucionDto>
    {
        private readonly IInstitucionService _institucionService;

        public CreateInstitucionDtoValidator(IInstitucionService institucionService)
        {
            _institucionService = institucionService;

            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre de la institución no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre de la institución no puede ser vacío.");

            When(institucion => !string.IsNullOrEmpty(institucion.Nombre), () =>
            {
                RuleFor(x => x.Nombre)
                .MustAsync(NombreInstitucionDisponible).WithMessage("Ya existe una institución con el mismo nombre");
            });

        }

        // <summary>
        /// Verifica si el nombre del institución está libre. (No existe en la base de datos).
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> NombreInstitucionDisponible(string? nombre, CancellationToken token)
        {
            if (string.IsNullOrEmpty(nombre))
                return false;

            var instituciones = await _institucionService.BuscarInstitucionesAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

            return instituciones.Any() == false;

        }
    }
}
