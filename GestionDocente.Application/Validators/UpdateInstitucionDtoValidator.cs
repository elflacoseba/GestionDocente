using FluentValidation;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Interfaces;

namespace GestionDocente.Application.Validators
{
    public class UpdateInstitucionDtoValidator : AbstractValidator<UpdateInstitucionDto>
    {
        private readonly IInstitucionService _institucionService;

        public UpdateInstitucionDtoValidator(IInstitucionService institucionService)
        {
            _institucionService = institucionService;

            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre de la institución no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre de la institución no puede ser vacío.");

            When(institucion => !string.IsNullOrEmpty(institucion.Nombre), () =>
            {
                RuleFor(x => x.Nombre)
               .MustAsync(async (institucion, nombre, cancellationToken) =>
               {
                   var institucionId = institucion.GetId();

                   var instituciones = await _institucionService.BuscarInstitucionesAsync(x => (x.Nombre.ToUpper() == nombre!.ToUpper()) &&     (x.Id != institucionId)
                   );
                   
                   return instituciones.Any() == false;

               }).WithMessage("Ya existe una institución con el mismo nombre");
            });

            RuleFor(x => x.Email)
               .NotNull().WithMessage("El email no puede ser nulo.")
               .NotEmpty().WithMessage("El email no puede estar vacío.")
               .EmailAddress().WithMessage("El texto no tiene el formato válido de una dirección de correo electrónico.")
               .MaximumLength(50).WithMessage("El email puede contener hasta {MaxLength} caracteres como máximo.");

        }

    }
}
