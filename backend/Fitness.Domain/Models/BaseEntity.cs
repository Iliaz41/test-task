namespace Fitness.Domain.Models
{
    public class BaseEntity
    {
        public long Id {  get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid(); // default
    }
}
