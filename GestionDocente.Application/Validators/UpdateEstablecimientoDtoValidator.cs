using FluentValidation;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Interfaces;

namespace GestionDocente.Application.Validators
{
    public class UpdateEstablecimientoDtoValidator : AbstractValidator<UpdateEstablecimientoDto>
    {
        private readonly IEstablecimientoService _establecimientoService;

        public UpdateEstablecimientoDtoValidator(IEstablecimientoService establecimientoService)
        {
            _establecimientoService = establecimientoService;

            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre del establecimiento no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre del establecimiento no puede ser vacío.");

            When(Establecimiento => !string.IsNullOrEmpty(Establecimiento.Nombre), () =>
            {
                RuleFor(x => x.Nombre)
               .MustAsync(async (establecimiento, nombre, cancellationToken) =>
               {
                   var establecimientoId = establecimiento.GetId();

                   var establecimientos = await _establecimientoService.BuscarEstablecimientosAsync(x =>
                                                                                                        (x.Nombre.ToUpper() == nombre!.ToUpper()) &&
                                                                                                        (x.Id != establecimientoId)
                                                                                                    );

                   return establecimientos.Any() == false;

               }).WithMessage("Ya existe un establecimiento con el mismo nombre");
            });

        }

    }
}
