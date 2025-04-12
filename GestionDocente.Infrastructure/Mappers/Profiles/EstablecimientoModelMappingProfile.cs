using AutoMapper;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Models;

namespace GestionDocente.Infrastructure.Mappers.Profiles
{
    public class EstablecimientoModelMappingProfile : Profile
    {
        public EstablecimientoModelMappingProfile()
        {
            CreateMap<EstablecimientoModel, Establecimiento>()
                .ReverseMap();
        }
    }
}
