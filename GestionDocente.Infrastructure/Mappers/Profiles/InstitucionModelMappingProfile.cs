using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Infrastructure.Models;

namespace GestionDocente.Infrastructure.Mappers.Profiles
{
    public class InstitucionModelMappingProfile : Profile
    {
        public InstitucionModelMappingProfile()
        {
            CreateMap<InstitucionModel, Institucion>()
                .ReverseMap();
        }
    }
}
