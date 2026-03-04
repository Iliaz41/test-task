namespace Fitness.Domain.Models
{
    public class Calendar : BaseEntity
    {
        public DateTime Day { get; set; }

        public bool IsToday => Day.Date == DateTime.UtcNow.Date;

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
