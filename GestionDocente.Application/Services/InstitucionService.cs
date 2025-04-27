using AutoMapper;
using GestionDocente.Application.Dtos.Request;
using GestionDocente.Application.Dtos.Response;
using GestionDocente.Application.Exceptions;
using GestionDocente.Application.Interfaces;
using GestionDocente.Application.Validators;
using GestionDocente.Domain.Entities;
using GestionDocente.Domain.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace GestionDocente.Application.Services
{
    public class InstitucionService : IInstitucionService
    {
        private readonly IInstitucionRepository _institucionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstitucionService(IInstitucionRepository institucionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _institucionRepository = institucionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<InstitucionResponseDto>> GetInstitucionesAsync()
        {
            var institucionesEntity =  await _institucionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<InstitucionResponseDto>>(institucionesEntity);
        }

        public async Task<InstitucionResponseDto?> GetInstitucionByIdAsync(string id)
        {
            var institucionEntity =  await _institucionRepository.GetByIdAsync(id);

            return _mapper.Map<InstitucionResponseDto>(institucionEntity);
        }

        public async Task<IEnumerable<InstitucionResponseDto>> BuscarInstitucionesAsync(Expression<Func<Institucion, bool>> predicate)
        {
            var resultados = await _institucionRepository.SearchAsync(predicate);

            return _mapper.Map<IEnumerable<InstitucionResponseDto>>(resultados);
        }

        public async Task<InstitucionResponseDto> CreateInstitucionAsync(CreateInstitucionDto createInstitucionDto)
        {
            var rules = new CreateInstitucionDtoValidator(this);

            var validationResult = await rules.ValidateAsync(createInstitucionDto);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var institucionEntity = _mapper.Map<Institucion>(createInstitucionDto);

            institucionEntity.FechaCreacion = DateTime.UtcNow;

            await _institucionRepository.AddAsync(institucionEntity);

           var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                throw new Exception("Error al guardar la institucion.");
            }

            return _mapper.Map<InstitucionResponseDto>(institucionEntity);
        }

        public async Task<InstitucionResponseDto> UpdateInstitucionAsync(UpdateInstitucionDto updateInstitucionDto)
        {
            var validationUpdateRules = new UpdateInstitucionDtoValidator(this);

            var validationResult = await validationUpdateRules.ValidateAsync(updateInstitucionDto);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage)).ToList();

                throw new ValidationException(errorValidations);
            }

            var institucionEntity = await _institucionRepository.GetByIdAsync(updateInstitucionDto.GetId()!);

            _mapper.Map(updateInstitucionDto, institucionEntity);

            institucionEntity!.FechaActualizacion = DateTime.UtcNow;

            await _institucionRepository.Update(institucionEntity!);

            var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                throw new Exception("Error al guardar la institución.");
            }

            return _mapper.Map<InstitucionResponseDto>(institucionEntity);
        }

        public async Task<bool> DeleteInstitucionAsync(string id)
        {
            await _institucionRepository.Delete(id);

           return  await _unitOfWork.CommitAsync();
        }
      
    }
}
