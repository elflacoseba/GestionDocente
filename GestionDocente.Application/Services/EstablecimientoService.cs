using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Exceptions;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Validators;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Domain.Models;
using System.Data;
using System.Linq.Expressions;

namespace GestionDocente.Application.Services
{
    public class EstablecimientoService : IEstablecimientoService
    {
        private readonly IEstablecimientoRepository _establecimientoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstablecimientoService(IEstablecimientoRepository establecimientoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _establecimientoRepository = establecimientoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<EstablecimientoResponseDto>> GetEstablecimientosAsync()
        {
            var establecimientosEntity =  await _establecimientoRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EstablecimientoResponseDto>>(establecimientosEntity);
        }

        public async Task<EstablecimientoResponseDto> GetEstablecimientosByIdAsync(Guid id)
        {
            var establecimiento =  await BuscarEstablecimientosAsync(x => x.Id.Equals(id.ToString()));

            return _mapper.Map<EstablecimientoResponseDto>(establecimiento.FirstOrDefault());
        }

        public async Task<IEnumerable<EstablecimientoResponseDto>> BuscarEstablecimientosAsync(Expression<Func<EstablecimientoModel, bool>> predicate)
        {
            var resultados = await _establecimientoRepository.SearchAsync(predicate);

            return _mapper.Map<IEnumerable<EstablecimientoResponseDto>>(resultados);
        }

        public async Task<EstablecimientoResponseDto> CreateEstablecimientoAsync(CreateEstablecimientoDto createEstablecimientoDto)
        {
            var rules = new CreateEstablecimientoDtoValidator(this);

            var validationResult = await rules.ValidateAsync(createEstablecimientoDto);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var establecimiento = _mapper.Map<Establecimiento>(createEstablecimientoDto);

            establecimiento.FechaCreacion = DateTime.UtcNow;

            await _establecimientoRepository.AddAsync(establecimiento);

           var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                throw new Exception("Error al guardar el establecimiento.");
            }

            return _mapper.Map<EstablecimientoResponseDto>(establecimiento);
        }

        public async Task<EstablecimientoResponseDto> UpdateEstablecimientoAsync(UpdateEstablecimientoDto updateEstablecimientoDto)
        {
            var validationUpdateRules = new UpdateEstablecimientoDtoValidator(this);

            var validationResult = await validationUpdateRules.ValidateAsync(updateEstablecimientoDto);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var establecimientoEntity = await _establecimientoRepository.GetByIdAsync(updateEstablecimientoDto.GetId()!);

            _mapper.Map(updateEstablecimientoDto, establecimientoEntity);

            establecimientoEntity!.FechaActualizacion = DateTime.UtcNow;

            await _establecimientoRepository.Update(establecimientoEntity!);

            var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                throw new Exception("Error al guardar el establecimiento.");
            }

            return _mapper.Map<EstablecimientoResponseDto>(establecimientoEntity);
        }

        public async Task<bool> DeleteEstablecimientoAsync(Guid id)
        {
            await _establecimientoRepository.Delete(id.ToString());

           return  await _unitOfWork.CommitAsync();
        }
      
        public Task UpdateEstablecimientoAsync(EstablecimientoRequestDto establecimientoRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
