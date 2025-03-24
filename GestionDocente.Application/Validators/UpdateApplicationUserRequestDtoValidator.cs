using FluentValidation;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Domain.Interfaces;

namespace GestionDocente.Application.Validators
{
    public class UpdateApplicationUserRequestDtoValidator : AbstractValidator<UpdateApplicationUserRequestDto>
    {        
        private readonly IApplicationUserRepository _userRepository;

        public UpdateApplicationUserRequestDtoValidator(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("El nombre de usuario no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre de usuario no puede ser vacío.")
                .MustAsync(async (user, userName, cancellationToken) =>
                {
                    var userEntity = await _userRepository.GetUserByUsernameAsync(userName!);

                    if (userEntity != null && userEntity.Id != user.GetId())
                    {
                        return false;
                    }

                    return true;
                }).WithMessage("Ya existe el nombre de usuario en el sistema.");

            RuleFor(x => x.Email)
               .NotNull().WithMessage("El email no puede ser nulo.")
               .NotEmpty().WithMessage("El email no puede estar vacío.")
               .EmailAddress().WithMessage("El texto no tiene el formato válido de una dirección de correo electrónico.")
               .MaximumLength(50).WithMessage("El email puede contener hasta {MaxLength} caracteres como máximo.")
               .MustAsync(async (user, email, cancellationToken) =>
               {
                   var userEntity = await _userRepository.GetUserByEmailAsync(email!);

                   if (userEntity != null && userEntity.Id != user.GetId())
                   {
                       return false;
                   }

                   return true;
               }).WithMessage("El email ya existe en el sistema.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("El nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre puede contener hasta {MaxLength} caracteres como máximo.");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("El apellido no puede ser nulo.")
                .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                .MaximumLength(50).WithMessage("El apellido puede contener hasta {MaxLength} caracteres como máximo.");
        }
    }
}
