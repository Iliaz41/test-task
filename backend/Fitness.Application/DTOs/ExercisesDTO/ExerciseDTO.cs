using Fitness.Domain.Models;

namespace Fitness.Application.DTOs.ExercisesDTO
{
    public class ExerciseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Repetitions { get; set; }
        public int Approaches { get; set; }
        public long UserId { get; set; }
        public long CalendarId { get; set; }
        public long MeasurementUnitId { get; set; }
        public long StatusId { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; } = null!;
        public Status Status { get; set; } = null!;
    }
}
