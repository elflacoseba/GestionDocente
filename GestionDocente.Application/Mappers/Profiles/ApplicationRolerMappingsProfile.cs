using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;

namespace GestionDocente.Application.Mappers.Profiles
{
    public class ApplicationRoleMappingsProfile : Profile
    {
        public ApplicationRoleMappingsProfile()
        {
            CreateMap<CreateApplicationRoleRequestDto, ApplicationRole>();

            CreateMap<UpdateApplicationRoleRequestDto, ApplicationRole>();

            CreateMap<ApplicationRole, RoleResponseDto>();
        }
    }
}
