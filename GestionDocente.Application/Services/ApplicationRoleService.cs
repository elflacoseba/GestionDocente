using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Exceptions;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Validators;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using ValidationException = GestionDocente.Application.Exceptions.ValidationException;

namespace GestionDocente.Application.Services
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IApplicationRoleRepository _roleRepository;
        private readonly IMapper _mapper;        

        public ApplicationRoleService(IApplicationRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;            
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRolesAsync()
        {
            var rolesEntity = await _roleRepository.GetRolesAsync();

            return _mapper.Map<IEnumerable<RoleResponseDto>>(rolesEntity);
        }
        
        public async Task<RoleResponseDto?> GetRoleByIdAsync(string roleId)
        {
            var roleEntity = await _roleRepository.GetRoleByIdAsync(roleId);

            return _mapper.Map<RoleResponseDto>(roleEntity);
        }

        public async Task<RoleResponseDto?> GetRoleByNameAsync(string roleName)
        {
            var roleEntity = await _roleRepository.GetRoleByNameAsync(roleName);

            return _mapper.Map<RoleResponseDto>(roleEntity);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleRepository.RoleExistsAsync(roleName);
        }

        public async Task<string> CreateRoleAsync(CreateApplicationRoleRequestDto role)
        {
            
            var rules = new CreateApplicationRoleRequestDtoValidator(_roleRepository);

            var validationResult = await rules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var roleEntity = _mapper.Map<ApplicationRole>(role);

            return await _roleRepository.CreateRoleAsync(roleEntity);
        }

        public async Task<bool> UpdateRoleAsync(string roleId, UpdateApplicationRoleRequestDto role)
        {
            var validationUpdateRules = new UpdateApplicationRoleRequestDtoValidator(_roleRepository);

            //paso el Id al Dto para que pueda ser validado
            role.SetId(roleId);

            var validationResult = await validationUpdateRules.ValidateAsync(role);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var roleEntity = _mapper.Map<ApplicationRole>(role);

            roleEntity.Id = roleId;

            return await _roleRepository.UpdateRoleAsync(roleEntity);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            return await _roleRepository.DeleteRoleAsync(roleId);
        }
                
    }
}
