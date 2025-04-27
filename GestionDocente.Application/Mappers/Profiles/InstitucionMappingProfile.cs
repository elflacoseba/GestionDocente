using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;

namespace GestionDocente.Application.Mappers.Profiles
{
    public class InstitucionMappingProfile : Profile
    {
        public InstitucionMappingProfile()
        {
            CreateMap<Institucion, InstitucionResponseDto>();

            CreateMap<CreateInstitucionDto, Institucion>();

            CreateMap<UpdateInstitucionDto, Institucion>();
        }
    }    
}
