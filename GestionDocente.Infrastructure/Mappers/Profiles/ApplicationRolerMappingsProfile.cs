using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Infrastructure.Models;

namespace GestionDocente.Infrastructure.Mappers.Profiles
{
    public class ApplicationRoleMappingsProfile : Profile
    {
        public ApplicationRoleMappingsProfile()
        {
            CreateMap<ApplicationRoleModel, ApplicationRole>()
                .ReverseMap();
        }
    }
}
