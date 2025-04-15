using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Exceptions;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Validators;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using ValidationException = GestionDocente.Application.Exceptions.ValidationException;

namespace GestionDocente.Application.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IApplicationRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationRoleService(IApplicationRoleRepository roleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ApplicationRoleResponseDto>> GetRolesAsync()
        {
            var rolesEntity = await _roleRepository.GetRolesAsync();

            return _mapper.Map<IEnumerable<ApplicationRoleResponseDto>>(rolesEntity);
        }
        
        public async Task<ApplicationRoleResponseDto?> GetRoleByIdAsync(string roleId)
        {
            var roleEntity = await _roleRepository.GetRoleByIdAsync(roleId);

            return _mapper.Map<ApplicationRoleResponseDto>(roleEntity);
        }

        public async Task<ApplicationRoleResponseDto?> GetRoleByNameAsync(string roleName)
        {
            var roleEntity = await _roleRepository.GetRoleByNameAsync(roleName);

            return _mapper.Map<ApplicationRoleResponseDto>(roleEntity);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleRepository.RoleExistsAsync(roleName);
        }

        public async Task<ApplicationRoleResponseDto> CreateRoleAsync(CreateApplicationRoleRequestDto role)
        {
            var rules = new CreateApplicationRoleRequestDtoValidator(_roleRepository);

            var validationResult = await rules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var applicationRol = new ApplicationRole{
                Name = role.Name,                               
                Description = role.Description
            };           

            await _roleRepository.CreateRoleAsync(applicationRol);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ApplicationRoleResponseDto>(applicationRol);
        }

        public async Task<bool> UpdateRoleAsync(UpdateApplicationRoleRequestDto role)
        {
            var validationUpdateRules = new UpdateApplicationRoleRequestDtoValidator(_roleRepository);

            var validationResult = await validationUpdateRules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var roleDB = await _roleRepository.GetRoleByIdAsync(role.GetId()!);

            _mapper.Map(role, roleDB);

            await _roleRepository.UpdateRoleAsync(roleDB!);

           return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleModel = await _roleRepository.GetRoleByIdAsync(roleId);

            if (roleModel == null)
            {
                throw new ValidationException(new List<ErrorValidation>
                {
                    new ErrorValidation("Id", "Rol no encontrado")
                });
            }

            return await _roleRepository.DeleteRoleAsync(roleId);            
        }

    }
}
