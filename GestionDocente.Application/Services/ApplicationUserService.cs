using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Exceptions;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Validators;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;

namespace GestionDocente.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateApplicationUserRequestDtoValidator _applicationUserRequestDtoValidationRules;
        private readonly UpdateApplicationUserRequestDtoValidator _updateApplicationUserRequestDtoValidator;

        public ApplicationUserService(IApplicationUserRepository userRepository, IMapper mapper, CreateApplicationUserRequestDtoValidator applicationUserRequestDtoValidationRules, UpdateApplicationUserRequestDtoValidator updateApplicationUserRequestDtoValidator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _applicationUserRequestDtoValidationRules = applicationUserRequestDtoValidationRules;
            _updateApplicationUserRequestDtoValidator = updateApplicationUserRequestDtoValidator;
            _unitOfWork = unitOfWork;
        }

        #region

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {

            var usersEntity = await _userRepository.GetUsersAsync();

            return _mapper.Map<IEnumerable<UserResponseDto>>(usersEntity);
        }

        public async Task<UserResponseDto?> GetUserByUsernameAsync(string username)
        {
            var userEntity = await _userRepository.GetUserByUsernameAsync(username);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(email);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(string userId)
        {
            var userEntity = await _userRepository.GetUserByIdAsync(userId);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<string> CreateUserAsync(CreateApplicationUserRequestDto user)
        {
     
            var validationResult = await _applicationUserRequestDtoValidationRules.ValidateAsync(user);           

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
                                             .ToList();

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el username en la base de datos
            var userUsername = await _userRepository.GetUserByUsernameAsync(user.UserName!);

            if (userUsername != null)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.UserName), "El nombre de usuario ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el email en la base de datos
            var userEmail = await _userRepository.GetUserByEmailAsync(user.Email!);

            if (userEmail != null)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.Email), "El email ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            var userEnity = _mapper.Map<ApplicationUser>(user);

            var resultCreateUser =  await _userRepository.CreateUserAsync(userEnity, user.Password!);

            if (resultCreateUser)
            {
                //Commit a la base de datos
                return await _unitOfWork.CommitAsync();
            }
            else
            {
                //Rollback a la base de datos
                await _unitOfWork.RollbackAsync();

                return false;
            }            
                        
        }

        public async Task<bool> UpdateUserAsync(string userId, UpdateApplicationUserRequestDto user)
        {
            //paso el Id al Dto para que pueda ser validado
            user.SetId(userId);

            var validationRules = new UpdateApplicationUserRequestDtoValidator(_userRepository);

            var validationResult = await _updateApplicationUserRequestDtoValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
                                             .ToList();

                throw new ValidationException(errorValidations);
            }           

            var userEntity = _mapper.Map<ApplicationUser>(user);

            return await _userRepository.UpdateUserAsync(userEntity!);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        #endregion

        #region Roles

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            return await _userRepository.GetRolesAsync(userId);
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            return await _userRepository.AddToRoleAsync(userId, roleName);
        }

        public Task<bool> AddToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            return _userRepository.AddToRolesAsync(userId, roleNames);
        }

        public async Task<bool> RemoveFromRoleAsync(string userId, string roleName)
        {
            return await _userRepository.RemoveFromRoleAsync(userId, roleName);
        }

        public async Task<bool> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            return await _userRepository.RemoveFromRolesAsync(userId, roleNames);
        }

        #endregion
    }
}
