using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Models;

namespace GestionDocente.Infrastructure.Mappers.Profiles
{
    public class ApplicationUserMappingsProfile : Profile
    {
        public ApplicationUserMappingsProfile()
        {
            CreateMap<ApplicationUserModel, ApplicationUser>()
                .ReverseMap();
        }
    }
}
