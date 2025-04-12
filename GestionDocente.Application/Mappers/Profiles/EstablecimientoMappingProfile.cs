using AutoMapper;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Domain.Entities;

namespace GestionDocente.Application.Mappers.Profiles
{
    public class EstablecimientoMappingProfile : Profile
    {
        public EstablecimientoMappingProfile()
        {
            CreateMap<Establecimiento, EstablecimientoDto>();                
        }
    }    
}
