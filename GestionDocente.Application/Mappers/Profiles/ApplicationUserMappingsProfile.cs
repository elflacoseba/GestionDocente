using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;

namespace GestionDocente.Application.Mappers.Profiles
{
    public class ApplicationUserMappingsProfile : Profile
    {
        public ApplicationUserMappingsProfile()
        {
            CreateMap<CreateApplicationUserRequestDto, ApplicationUser>();

            CreateMap<UpdateApplicationUserRequestDto, ApplicationUser>();

            CreateMap<ApplicationUser, UserResponseDto>();
        }
    }
}
