using Fitness.Application.DTOs.ExercisesDTO;

namespace Fitness.Application.IServices
{
    public interface IExerciseSerivce
    {
        Task<ExerciseDTO> CreateExerciseAsync(CreateExerciseDTO createExerciseDTO, CancellationToken tokenCancel);
        Task<List<ExerciseDTO?>> GetAllExercisesAsync(CancellationToken tokenCancel);
        Task<ExerciseDTO> UpdateExerciseAsync(long id, UpdateExerciseDTO exerciseDTO, CancellationToken tokenCancel);
        Task DeleteExerciseAsync(long id, CancellationToken tokenCancel);
        Task<ExerciseDTO?> GetExerciseByIdAsync(long id, CancellationToken tokenCancel);
        Task<List<ExerciseDTO?>> GetExercisesByUserAndDay(long userId, DateTime day, CancellationToken tokenCancel);
    }
}
