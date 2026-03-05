using AutoMapper;
using Fitness.Application.DTOs.ExercisesDTO;
using Fitness.Application.IRepositories;
using Fitness.Application.IServices;
using Fitness.Domain.Exceptions;
using Fitness.Domain.Models;

namespace Fitness.Application.Services
{
    public class ExerciseService : IExerciseSerivce
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<ExerciseDTO> CreateExerciseAsync(CreateExerciseDTO createExerciseDTO, CancellationToken tokenCancel)
        {
            var exercise = _mapper.Map<Exercise>(createExerciseDTO);

            await _exerciseRepository.CreateAsync(entity: exercise, token: tokenCancel);

            return _mapper.Map<ExerciseDTO>(exercise);
        }

        public async Task<List<ExerciseDTO>> GetAllExercisesAsync(CancellationToken tokenCancel)
        {
            var exercises = await _exerciseRepository.GetAllAsync(token: tokenCancel);
            var exercisesDTOs = _mapper.Map<List<ExerciseDTO>>(exercises);

            return exercisesDTOs;
        }

        public async Task<ExerciseDTO> UpdateExerciseAsync(long id, UpdateExerciseDTO exerciseDTO, CancellationToken tokenCancel)
        {
            if (exerciseDTO.Id != id)
            {
                throw new BadRequestException("Id mismatch");
            }

            var exercise = await _exerciseRepository.GetByIdAsync(id: exerciseDTO.Id, token: tokenCancel)
                ?? throw new NotFoundException($"Exercise with id {exerciseDTO.Id} is not found.");

            exercise = _mapper.Map(exerciseDTO, exercise);
            await _exerciseRepository.Update(entity: exercise, token: tokenCancel);

            return _mapper.Map<ExerciseDTO>(exercise);
        }

        public async Task DeleteExerciseAsync(long id, CancellationToken tokenCancel)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"Exercise with Id ({id}) is not found");

            await _exerciseRepository.Delete(exercise, tokenCancel);
        }

        public async Task<ExerciseDTO?> GetExerciseByIdAsync(long id, CancellationToken tokenCancel)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"Exercise with Id ({id}) is not found.");

            var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);

            return exerciseDTO;
        }

        public async Task<List<ExerciseDTO?>> GetExercisesByUserAndDay(long userId, DateTime day, CancellationToken tokenCancel)
        {
            var exercises = await _exerciseRepository.GetUserExercisesForDayAsync(userId: userId, day: day, tokenCancel: tokenCancel);

            var exercisesDTO = _mapper.Map<List<ExerciseDTO>>(exercises);

            return exercisesDTO; 
        }
    }
}
